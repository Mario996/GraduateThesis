using ElasticPMTServer.Models;
using ElasticPMTServer.Models.Pokusaj;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ElasticPMTServer.Repositories
{
    public class Repository : IRepository
    {
        protected readonly ElasticClient _elasticClient;
        private readonly ConnectionSettings _settings;

        public Repository()
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

           _elasticClient.Indices.Create("jsonindex", c => c
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

            return false;
        }

        public IndexResponse create()
        {
            var json = File.ReadAllText("C:\\Users\\Mario\\Desktop\\NIST-LOW-BASELINE-PROFILE.json");
            IndexResponse response = new IndexResponse();
            if (!indexExists())
            {
                Root root = JsonConvert.DeserializeObject<Root>(json);

                foreach (Group group in root.Catalog.Groups)
                {
                    string groupTitle = group.Title;
                    foreach (Control control in group.Controls)
                    {
                        string controlId = control.Id;
                        string controlClass = control.Class;
                        string controlTitle = control.Title;
                        foreach (Part part in control.Parts)
                        {
                            if (part.Parts != null)
                            {
                                foreach (Part innerPart in part.Parts)
                                {
                                    if (innerPart.Parts != null)
                                    {
                                        foreach (Part doubleInnerPart in innerPart.Parts)
                                        {
                                            if (doubleInnerPart.Parts != null)
                                            {
                                                foreach (Part tripleInnerPart in doubleInnerPart.Parts)
                                                {
                                                    if (tripleInnerPart.Parts != null)
                                                    {
                                                        foreach (Part quadrupleInnerPart in tripleInnerPart.Parts)
                                                        {
                                                            string partId = quadrupleInnerPart.Id;
                                                            string partProse = quadrupleInnerPart.Prose;
                                                            CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                            _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string partId = tripleInnerPart.Id;
                                                        string partProse = tripleInnerPart.Prose;
                                                        CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                        _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string partId = doubleInnerPart.Id;
                                                string partProse = doubleInnerPart.Prose;
                                                CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string partId = innerPart.Id;
                                        string partProse = innerPart.Prose;
                                        CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                        _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                    }
                                }
                            }
                            else
                            {
                                string partId = part.Id;
                                string partProse = part.Prose;
                                CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                response = _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                            }
                        }
                    }
                }
            }

            return response;
        }

        //public DeleteResponse delete(string id)
        //{
        //    return _elasticClient.Delete<TEntity>(id);
        //}

        //public ISearchResponse<TEntity> getAll()
        //{
        //    _elasticClient.Indices.Refresh();
        //    return _elasticClient.Search<TEntity>(s => s
        //       .MatchAll()
        //    );
        //}

        //public GetResponse<TEntity> getById(string id)
        //{
        //    _elasticClient.Indices.Refresh();
        //    return _elasticClient.Get<TEntity>(id);
        //}

        //public UpdateResponse<TEntity> update(string id, TEntity document)
        //{
        //    return _elasticClient.Update<TEntity>(id, u => u
        //      .Doc(document)
        //      .Refresh(Elasticsearch.Net.Refresh.True)
        //    );
        //}
    }
}
