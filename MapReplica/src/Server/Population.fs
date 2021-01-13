module Population

open FSharp.Data
open DataProviders
open Shared


let readPopulation (path: string) =
    let popolazione = Popolazione.Load path

    popolazione.Rows
    |> Seq.map (fun row -> row.``Codice provincia`` |> GeoCode, row.``Maschi + Femmine``)
    |> Map.ofSeq


//versione Asyncrona
