#r "nuget: FSharp.Data"

#load "../src/Shared/GeoShared.fs"

let absolutePath relative = __SOURCE_DIRECTORY__ + relative

[<Literal>]
let FilePandemia =
    __SOURCE_DIRECTORY__
    + @"\dpc-covid19-ita-province.csv"

open FSharp.Data
type Pandemia = CsvProvider<FilePandemia>

//data,stato,codice_regione,denominazione_regione,codice_provincia,denominazione_provincia,sigla_provincia,lat,long,totale_casi,note

let pandemia = Pandemia.Load(FilePandemia)

let rows = pandemia.Rows


open System

fsi.AddPrinter<DateTime>(fun d -> d.ToShortDateString())

rows
|> Seq.filter (fun row -> row.Codice_provincia < 900)
|> Seq.map (fun row -> row.Data, row.Codice_provincia, row.Sigla_provincia, row.Totale_casi)
|> Seq.toArray
