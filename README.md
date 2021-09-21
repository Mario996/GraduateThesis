# GraduateThesis
Tool that uses ElasticSearch to find similarities in security requirements from different security standards

Steps to get the project up and running are below, should be done from top to bottom:

# Setup ElasticSearch
1. Make sure you have .NET Core 2.2 installed on your machine, if not present, download it from: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-2.2.200-preview-windows-x86-installer
2. Download and install ElasticSearch 7.6.2 from: https://www.elastic.co/downloads/past-releases/elasticsearch-7-6-2
3. Run ElasticSearch by running elasticsearch.bat found in bin folder (should be on port 9200, which is the default)

# Setup ElasticPMTServer
1. Run the project (should be on port 44332)

# Setup elastic-pmt
1. npm install
2. npm run serve
