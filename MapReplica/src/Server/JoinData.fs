module JoinData

open Covid
open Shared
open System

let private totalCasesInWeekTo covidData (date: DateTime) =
    let weekBefore = date.AddDays(-6.0)

    let countSofar =
        covidData
        |> Array.sortBy (fun cd -> cd.Date)
        |> Array.filter (fun cd -> cd.Date < weekBefore)
        |> Array.tryLast
        |> Option.map (fun cd -> cd.Count)
        |> Option.defaultValue 0

    covidData
    |> Array.filter (fun cd -> cd.Date >= weekBefore && cd.Date <= date)
    |> Array.sumBy (fun cd -> (cd.Count - countSofar))


let private extractRates dates areaData population =

    let weeklyRates =
        dates
        |> List.map
            (fun date ->
                date,
                ((totalCasesInWeekTo areaData date) |> float)
                * 100000.0
                / (population |> float))

    { WeeklyCasesPer100k = Map.ofList weeklyRates }


let join dates (covidData: CovidData []) populations boundaries =
    let getArea (geoCode, name, boundary) =

        let population = Map.tryFind geoCode populations

        let areaData =
            covidData
            |> Array.filter (fun cd -> cd.GeoCode = geoCode)

        let covidRates =

            match population, Array.isEmpty areaData with
            | Some pop, false when pop > 0 -> extractRates dates areaData pop |> Some
            | _ -> None

        { GeoCode = geoCode
          Name = name
          Boundary = boundary
          Data = covidRates }

    boundaries |> Array.map getArea
