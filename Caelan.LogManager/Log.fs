namespace Caelan.LogManager

open System.Collections.Generic
open System.Configuration

module Log =
    let mutable private writers = Seq.empty<ILogWriter>
    let private conf = ConfigurationManager.GetSection("logManager") :?> LogConfiguration

    if conf <> null then
        let fileWriter =
            match conf.FileWriter with
            | null -> None
            | _ -> Some(FileLogWriter(conf.FileWriter.Path))

        writers <- [ fileWriter ] |> Seq.filter (fun t -> t.IsSome) |> Seq.map (fun t -> t.Value :> ILogWriter)

    let CurrentLogger<'T>() =
        Logger<'T>(writers)

    let Logger name =
        Logger(name, writers)