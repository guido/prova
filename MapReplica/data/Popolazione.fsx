#load "../src/Shared/GeoShared.fs"

#r "nuget: FSharp.Data"


let absolutePath relative = __SOURCE_DIRECTORY__ + relative

[<Literal>]
let FilePopilazione =
    __SOURCE_DIRECTORY__
    + @"\Popolazione_Tutte_le_province.csv"


open FSharp.Data


type Popolazione = CsvProvider<FilePopilazione, ";">

let popolazione = Popolazione.Load FilePopilazione

let rows = popolazione.Rows

//"Codice provincia";"Provincia";"Totale Maschi";"Totale Femmine";"Maschi + Femmine"
rows
|> Seq.map (fun row -> row.``Codice provincia``, row.Provincia, row.``Maschi + Femmine``)
|> Seq.toArray
