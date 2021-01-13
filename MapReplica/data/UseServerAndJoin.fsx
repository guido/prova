#r "nuget: FSharp.Data"
#r "nuget: SharpKml.Core"

#r @"C:\NCoding\MapReplica\src\DataProviders\bin\Debug\netcoreapp3.1\DataProviders.dll"
#load "../src/Shared/GeoShared.fs"
#load "../src/Server/Geography.fs"
#load "../src/Server/CovidData.fs"


open Shared
open Geography

open DataProviders

let pandemia =
    Pandemia.Load(@"C:\NCoding\MapReplica\data\dpc-covid19-ita-province.csv")

pandemia.GetSample()
