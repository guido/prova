namespace Shared

open System

type Loop = { LatLongs: (float * float) [] }

type Shape = { OuterBoundary: Loop; Holes: Loop [] }

type Boundary = { Shapes: Shape [] }

type CovidRates =
    { WeeklyCasesPer100k: Map<DateTime, float> }

//Codice Geografico ad esempio Provincia è un numero non la solita sigla
type GeoCode = GeoCode of int

type Area =
    { GeoCode: GeoCode
      Name: string
      Boundary: Boundary
      Data: CovidRates option }

//NB la firma deve essere Async per ogni metodo
type ICovidMapApi =
    { getDates: unit -> Async<DateTime []>
      getData: unit -> Async<Area []> }


module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName
