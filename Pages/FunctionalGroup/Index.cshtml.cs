using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReadAcross.ChemInfo;
using System.Drawing.Text;
using NuGet.Protocol;
using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;

namespace ReadAcross.Pages_FunctionalGroup
{
    public class IndexModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public IndexModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public IList<FunctionalGroup> FunctionalGroup { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            var fGroups = from fg in _context.FunctionalGroup
                          select fg;

            // Create a string for the Smiles value
            string? smileString = SearchString;

            if (!string.IsNullOrEmpty(smileString))
            {
                // Test for a CAS Number using a Regex 
                //Instantiate the regular expression object.
                string pattern = @"^\d{1,7}\-\d{2}\-\d$";
                System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Match the regular expression pattern against a text string.
                System.Text.RegularExpressions.Match m = r.Match(smileString);
                if (m.Success)
                {
                    //string url = "https://commonchemistry.cas.org/api/detail?cas_rn=108-88-3" + SearchString;
                    HttpClient httpClient = new()
                    {
                        BaseAddress = new Uri("https://commonchemistry.cas.org"),
                    };
                    var comChem = await httpClient.GetFromJsonAsync<CommonChemistry>("api/detail?cas_rn=" + SearchString);
                    if (!(comChem is null)) smileString = comChem.smile;
                }

                if (!smileString.IsNullOrEmpty())
                {

                    ChemInfo.Molecule molecule = new ChemInfo.Molecule(smileString);
                    if (molecule.Atoms.Length > 0)
                    {
                        List<FunctionalGroup> fgs = TestMolecule(smileString, fGroups.ToList<FunctionalGroup>());
                        fGroups = fGroups.Where(s => fgs.Contains(s));
                    }
                }
            }
            FunctionalGroup = await fGroups.ToListAsync();
        }

        private bool IsFunctionalGroupInMolecule(Molecule molecule, FunctionalGroup functionalGroup)
        {
            //if (functionalGroup.Name == "AROMATIC" && molecule.Aromatic) return true;
            if (functionalGroup.Name == "HETEROCYCLE" && molecule.Heterocyclic) return true;
            if (functionalGroup.Name == "HETEROCYCLIC AROMATIC" && molecule.HeterocyclicAromatic) return true;
            if (!string.IsNullOrEmpty(functionalGroup.Smarts))
            {
                if (molecule.FindFunctionalGroup(functionalGroup)) return true;
            }
            return false;
        }

        private List<FunctionalGroup> TestMolecule(string smilesSearchString, List<FunctionalGroup> functionalGroups)
        {
            List<FunctionalGroup> fgFound = new List<FunctionalGroup>();
            ChemInfo.Molecule? molecule = null;
            string IUPacName = string.Empty;
            string CASNo = string.Empty;
            string DTXSID = string.Empty;

            molecule = new ChemInfo.Molecule(smilesSearchString.Trim());
            if (molecule.Atoms.Length != 0)
            {
                foreach (var fg in functionalGroups)
                {
                    string smarts = fg.Smarts;
                    if (!string.IsNullOrEmpty(fg.Smarts))
                        if (molecule.FindFunctionalGroup(fg))
                        {
                            fgFound.Add(fg);
                        }
                    if (fg.Name == "AROMATIC" && molecule.Aromatic) fgFound.Add(fg);
                    if (fg.Name == "HETEROCYCLE" && molecule.Heterocyclic) fgFound.Add(fg);
                    if (fg.Name == "HETEROCYCLIC AROMATIC" && molecule.HeterocyclicAromatic) fgFound.Add(fg);
                }
            }
            return fgFound;
        }


        private bool TestCASNo(ref string CASNo, ref string smilesSearchString, ref string IUPacName, ref string DTXSID)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://actorws.epa.gov/actorws/chemIdentifier/v01/resolve.json?identifier=" + smilesSearchString);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            System.Runtime.Serialization.Json.DataContractJsonSerializer jSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Rootobject));
            Rootobject root = (Rootobject)jSerializer.ReadObject(response.GetResponseStream());
            if (string.IsNullOrEmpty(root.DataRow.smiles)) return false;
            CASNo = root.DataRow.casrn;
            smilesSearchString = root.DataRow.smiles;
            IUPacName = root.DataRow.preferredName;
            DTXSID = root.DataRow.dtxsid;
            return true;
        }
    }

    public record class CommonChemistry
    {
        public string? uri { get; set; }
        public string? rn { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
        public string? inchi { get; set; }
        public string? inchiKey { get; set; }
        public string? smile { get; set; }
        public string? canonicalSmile { get; set; }
        public string? molecularFormula { get; set; }
        public double? molecularMass { get; set; }
        public List<ExperimentalProperties>? experimentalProperties { get; set; }
        public List<PropertyCitations>? propertyCitations { get; set; }
        public List<string>? synonyms { get; set; }
    }

    public record class ExperimentalProperties
    {
        public string? name { get; set; }
        public string? property { get; set; }
        public int? sourceNumber { get; set; }
    }

    public record class PropertyCitations
    {
        public string? docUri { get; set; }
        public int? sourceNumber { get; set; }
        public string? source { get; set; }
    }


    public class Rootobject
    {
        public Datarow DataRow { get; set; }
    }

    public class Datarow
    {
        public string origIdentifier { get; set; }
        public string casrn { get; set; }
        public string preferredName { get; set; }
        public int synGsid { get; set; }
        public string synType { get; set; }
        public string synIdentifier { get; set; }
        public string dtxsid { get; set; }
        public string dtxcid { get; set; }
        public string jChemInChIKey { get; set; }
        public string indigoInChIKey { get; set; }
        public string smiles { get; set; }
        public string molFormula { get; set; }
        public float molWeight { get; set; }
        public object collidingGsid { get; set; }
        public object collidingCasrn { get; set; }
        public object collidingPreferredName { get; set; }
        public bool trimmedWhitespace { get; set; }
        public bool trimmedLeadingZeros { get; set; }
        public bool reformattedIdentifier { get; set; }
        public string checksum { get; set; }
        public string processedAs { get; set; }
        public object infoMsg { get; set; }
        public object warningMsg { get; set; }
        public string msReadyForms { get; set; }
        public string qsarForms { get; set; }
        public string imageURL { get; set; }
    }
}
