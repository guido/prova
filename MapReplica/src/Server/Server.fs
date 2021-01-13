open System
open System.IO

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared

let dates = [ DateTime(2020, 11, 1); DateTime(2020, 11, 8); DateTime(2020, 11, 15); DateTime(2020, 11, 22); DateTime(2020, 11, 29); DateTime(2020, 12, 6) ]

let getAllData () =

    let firstDate = (List.min dates).AddDays(-8.0)
    let lastDate = List.max dates
    let covidData = Covid.read @"C:\NCoding\MapReplica\src\Server\serverdata\dpc-covid19-ita-province.csv" firstDate lastDate
    
    let populations = Population.readPopulation @"C:\NCoding\MapReplica\src\Server\serverdata\Popolazione_Tutte_le_province.csv"
    let boundaries = Geography.readBoundaries @"C:\NCoding\MapReplica\src\Server\serverdata\province.kml"

    let joined = JoinData.join dates covidData populations boundaries
    joined

let covidMapApi =
    { getDates = fun () -> async { return List.toArray dates }
      getData = fun () -> async { return getAllData() }
    }


let webApp =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue covidMapApi
    |> Remoting.withDiagnosticsLogger (printfn "%s")
    |> Remoting.buildHttpHandler



let publicPath = System.IO.Path.GetFullPath "./public"

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x
let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let app =
    application {
        url ("http://localhost:" + port.ToString() + "/")
        use_router webApp
        memory_cache
        use_static publicPath
        use_gzip
    }

run app 