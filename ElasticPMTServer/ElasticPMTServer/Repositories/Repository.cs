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
using Task = ElasticPMTServer.Models.Task;

namespace ElasticPMTServer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ElasticSearchType
    {
        protected readonly ElasticClient _elasticClient;
        protected readonly ElasticClient _elasticJsonClient;
        private readonly ConnectionSettings _settings;
        private readonly ConnectionSettings _settingsJson;
        private readonly string _indexName;

        protected Repository(string indexName)
        {
            _settings = new ConnectionSettings()
                                .DefaultMappingFor<TEntity>(m => m
                                .IndexName(indexName)
                                .IdProperty(p => p.Id)
                                );

            _settingsJson = new ConnectionSettings()
                               .DefaultMappingFor<CustomControl>(m => m
                               .DisableIdInference()
                               .IndexName("jsonindex")
                               );

            _elasticClient = new ElasticClient(_settings);
            _elasticJsonClient = new ElasticClient(_settingsJson);
            _indexName = indexName;
        }

        public void checkIfIndexExists()
        {
            if (_elasticClient.Indices.Exists(_indexName).Exists)
            {
                return;
            }

            switch (_indexName)
            {
                case "requirements":
                    _elasticClient.Indices.Create(_indexName, c => c
                        .Settings(s => s
                            .NumberOfShards(1)
                        ).Map<Requirement>(r => r
                            .AutoMap()
                            .Properties(ps => ps
                                    .Nested<Comment>(n => n
                                        .Name(nn => nn.Comments)
                                        .AutoMap()
                                    )
                            )
                        ));
                    break;
                case "tasks":
                    _elasticClient.Indices.Create(_indexName, c => c
                       .Settings(s => s
                           .NumberOfShards(1)
                       ).Map<Task>(r => r
                           .AutoMap()
                           .Properties(ps => ps
                                   .Nested<Comment>(n => n
                                       .Name(nn => nn.Comments)
                                       .AutoMap()
                                   )
                                   .Nested<Label>(n => n
                                       .Name(nn => nn.Labels)
                                       .AutoMap()
                                   )
                           )
                       ));
                    break;
                default:
                    _elasticClient.Indices.Create(_indexName, c => c
                                                .Settings(s => s
                                                    .NumberOfShards(1))
                                                    .Map<TEntity>(m => m
                                                        .AutoMap()
                                                    )
                                                );
                    break;
            }
        }

        public IndexResponse create(TEntity document)
        {
            var json = File.ReadAllText("C:\\Users\\Mario\\Desktop\\NIST-LOW-BASELINE-PROFILE.json");
            //_elasticJsonClient.Indices.Create("jsonindex", c => c
            //           .Settings(s => s
            //               .NumberOfShards(1))
            //           .Map<CustomControl>(m => m.AutoMap()));
            var createIndexResponse = _elasticJsonClient.Indices.Create("jsonindex", c => c
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
                                                                        .MinGram(3)
                                                                        .MaxGram(9)
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
                                if(innerPart.Parts != null)
                                {
                                    foreach (Part doubleInnerPart in innerPart.Parts)
                                    {
                                        if (doubleInnerPart.Parts != null)
                                        {
                                            foreach (Part tripleInnerPart in doubleInnerPart.Parts)
                                            {
                                                if(tripleInnerPart.Parts != null)
                                                {
                                                    foreach (Part quadrupleInnerPart in tripleInnerPart.Parts)
                                                    {
                                                        string partId = quadrupleInnerPart.Id;
                                                        string partProse = quadrupleInnerPart.Prose;
                                                        CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                        var response = _elasticJsonClient.Index(newControl, i => i.Index("jsonindex"));                                                
                                                    }
                                                } else
                                                {
                                                    string partId = tripleInnerPart.Id;
                                                    string partProse = tripleInnerPart.Prose;
                                                    CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                    _elasticJsonClient.Index(newControl, i => i.Index("jsonindex"));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string partId = doubleInnerPart.Id;
                                            string partProse = doubleInnerPart.Prose;
                                            CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                            var response = _elasticJsonClient.Index(newControl, i => i.Index("jsonindex"));
                                            Console.WriteLine(response.IsValid);
                                        }
                                    }
                                } else 
                                {
                                    string partId = innerPart.Id;
                                    string partProse = innerPart.Prose;
                                    CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                    var response = _elasticJsonClient.Index(newControl, i => i.Index("jsonindex"));
                                    Console.WriteLine(response.IsValid);
                                }
                            }
                        }
                        else
                        {
                            string partId = part.Id;
                            string partProse = part.Prose;
                            CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                            var response = _elasticJsonClient.Index(newControl, i => i.Index("jsonindex"));
                            Console.WriteLine(response.IsValid);
                        }
                    }
                }
            }
            checkIfIndexExists();
            return _elasticClient.Index(document, i => i
                    .Refresh(Refresh.True));
        }

        public DeleteResponse delete(string id)
        {
            return _elasticClient.Delete<TEntity>(id);
        }

        public ISearchResponse<TEntity> getAll()
        {
            _elasticClient.Indices.Refresh();
            return _elasticClient.Search<TEntity>(s => s
               .MatchAll()
            );
        }

        public GetResponse<TEntity> getById(string id)
        {
            _elasticClient.Indices.Refresh();
            return _elasticClient.Get<TEntity>(id);
        }

        public UpdateResponse<TEntity> update(string id, TEntity document)
        {
            return _elasticClient.Update<TEntity>(id, u => u
              .Doc(document)
              .Refresh(Elasticsearch.Net.Refresh.True)
            );
        }
    }
}
