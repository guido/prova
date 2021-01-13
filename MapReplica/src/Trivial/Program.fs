// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp


open DataProviders
// Define a function to construct a message to print
let from whom = sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let pandemia =
        Pandemia.Load(@"C:\NCoding\MapReplica\data\dpc-covid19-ita-province.csv")

    pandemia.Rows
    |> Seq.map (fun x -> (x.Codice_provincia, x.Denominazione_provincia, x.Totale_casi))
    |> Seq.truncate 50
    |> Seq.iter (fun (c, d, n) -> printfn $"{c} {d} {n}")

    let popolazione =
        Popolazione.Load(@"C:\NCoding\MapReplica\data\Popolazione_Tutte_le_province.csv")

    printfn "--------------------"

    popolazione.Rows
    |> Seq.map (fun x -> (x.``Codice provincia``, x.Provincia, x.``Maschi + Femmine``))
    |> Seq.iter (fun (c, d, n) -> printfn $"{c} {d} {n}")


    printfn "--------------------"
    0 // return an integer exit code
