namespace DataProviders

open FSharp.Data

type Pandemia =
    CsvProvider<"embed/covid.csv", HasHeaders=true, EmbeddedResource="DataProviders, DataProviders.embed.covid.csv">

type Popolazione =
    CsvProvider<"embed/popolazione.csv", Separators=";", HasHeaders=true, EmbeddedResource="DataProviders, DataProviders.embed.popolazione.csv">
