module Covid

open Shared
open System
open DataProviders

type CovidData =
    { GeoCode: GeoCode
      Date: DateTime
      Count: int }

let readCovidData (path: string) =
    let pandemia = Pandemia.Load path

    pandemia.Rows
    |> Seq.filter (fun row -> row.Codice_provincia < 200)
    |> Seq.map
        (fun row ->
            { GeoCode = row.Codice_provincia |> GeoCode
              Date = row.Data
              Count = row.Totale_casi })


let read (path: string) start stop =
    path
    |> readCovidData
    |> Seq.filter (fun c -> c.Date >= start && c.Date <= stop)
    |> Seq.toArray
