using ElasticPMTServer.Models;
using ElasticPMTServer.Models.ElasticSearch;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ElasticPMTServer.Services
{
    public class IndexService : IIndexService
    {
        private readonly ElasticClient _elasticClient;
        private readonly ConnectionSettings _settings;
        private List<CustomControl> controlsForIndexing = new List<CustomControl>();
        private Dictionary<string, string> controlParameters = new Dictionary<string, string>();

        public IndexService()
        {
            _settings = new ConnectionSettings()
                             .DefaultMappingFor<CustomControl>(m => m
                               .DisableIdInference()
                               .IndexName("jsonindex")
                               );

            _elasticClient = new ElasticClient(_settings);
        }

        public bool indexExists()
        {
            if (_elasticClient.Indices.Exists("jsonindex").Exists)
            {
                return true;
            }
            return false;
        }

        public BulkResponse populateIndex()
        {
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Properties" , "NIST-LOW-BASELINE-PROFILE.json"));
            BulkResponse response = new BulkResponse();

            if (!indexExists())
            {
                var createIndexResponse = createIndex();
                if (!createIndexResponse.IsValid)
                {
                    return new BulkResponse();
                }
                Root root = JsonConvert.DeserializeObject<Root>(json);

                foreach (Group group in root.Catalog.Groups)
                {
                    string groupTitle = group.Title;
                    foreach (Control control in group.Controls)
                    {
                        string controlId = control.Id;
                        string controlClass = control.Class;
                        string controlTitle = control.Title;
                        if(control.Params != null)
                        {
                            foreach(Param parameter in control.Params)
                            {
                                controlParameters.Add(parameter.Id, parameter.Label);
                            }
                        }
                        foreach (Part part in control.Parts)
                        {
                            if (part.Parts != null)
                            {
                                addToListIfProseExists(groupTitle, controlId, controlClass, controlTitle, part.Id, part.Prose);
                                foreach (Part innerPart in part.Parts)
                                {
                                    if (innerPart.Parts != null)
                                    {
                                        addToListIfProseExists(groupTitle, controlId, controlClass, controlTitle, innerPart.Id, innerPart.Prose);
                                        foreach (Part doubleInnerPart in innerPart.Parts)
                                        {
                                            if (doubleInnerPart.Parts != null)
                                            {
                                                addToListIfProseExists(groupTitle, controlId, controlClass, controlTitle, doubleInnerPart.Id, doubleInnerPart.Prose);
                                                foreach (Part tripleInnerPart in doubleInnerPart.Parts)
                                                {
                                                    if (tripleInnerPart.Parts != null)
                                                    {
                                                        foreach (Part quadrupleInnerPart in tripleInnerPart.Parts)
                                                        {                                                      
                                                            controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, quadrupleInnerPart.Id, quadrupleInnerPart.Prose));
                                                        }
                                                    }
                                                    else
                                                    {                                                     
                                                        controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, tripleInnerPart.Id, tripleInnerPart.Prose));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, doubleInnerPart.Id, doubleInnerPart.Prose));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, innerPart.Id, innerPart.Prose));
                                    }
                                }
                            }
                            else
                            {                     
                                controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, part.Id, part.Prose));
                            }
                        }
                    }
                }
            }


            populateDynamicParameters();

            response = _elasticClient.IndexMany(controlsForIndexing, index: "jsonindex");

            return response;
        }

        CreateIndexResponse createIndex()
        {
            return _elasticClient.Indices.Create("jsonindex", c => c
                     .Settings(st => st
                         .Setting(UpdatableIndexSettings.MaxNGramDiff, 7)
                         .Analysis(an => an
                             .Analyzers(anz => anz
                                 .Custom("ngram_analyzer", na => na
                                 .Tokenizer("ngram_tokenizer")
                                 .Filters("lowercase"))
                                 )
                             .Tokenizers(tz => tz
                                 .NGram("ngram_tokenizer", td => td
                                     .MinGram(4)
                                     .MaxGram(5)
                                     .TokenChars(
                                         TokenChar.Letter,
                                         TokenChar.Digit,
                                         TokenChar.Symbol
                                     )
                                 )
                             )
                         )
                     )
                         .Map<CustomControl>(m => m.AutoMap())
                 );
        }

        private void addToListIfProseExists(string groupTitle, string controlId, string controlClass, string controlTitle, string partId, string partProse)
        {
            if(partProse != null)
            {
                controlsForIndexing.Add(new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse));
            }
        }

        private void populateDynamicParameters()
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("{{ insert: param, (.*?) }}");
            for (int i = 0; i <= controlsForIndexing.Count - 1; i++)
            {
                if (reg.Matches(controlsForIndexing[i].PartProse).Count > 0)
                {
                    string[] splits = reg.Split(controlsForIndexing[i].PartProse);
                    for (int j = 0; j <= splits.Length - 1; j++)
                    {
                        if (controlParameters.ContainsKey(splits[j]))
                        {
                            splits[j] = controlParameters[splits[j]];
                        }
                    }
                    controlsForIndexing[i].PartProse = String.Concat(splits);
                }
            }
        }
    }
}
