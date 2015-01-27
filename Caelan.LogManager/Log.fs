namespace Caelan.LogManager

open System.Collections.Generic
open System.Configuration
open System.Reflection
open System

module Log = 
    let private writersTypes = [ WriterType.File, typeof<FileLogWriter> ] |> Map.ofList
    //        Assembly.GetExecutingAssembly().GetTypes() 
    //        |> Seq.filter (fun t -> typeof<ILogW  riter>.IsAssignableFrom(t) && not t.IsInterface)
    let private conf = ConfigurationManager.GetSection("log") :?> LogConfiguration
    
    let private confWriters = 
        match conf with
        | null -> None
        | t when t.Writers.Count < 1 -> None
        | _ -> Some(conf.Writers)
    
    let private writers = 
        match confWriters with
        | None -> Seq.empty<ILogWriter>
        | Some(ws) -> 
            writersTypes
            |> Map.map (fun k t -> Activator.CreateInstance(t, ws.Item(k.ToString())) :?> ILogWriter)
            |> Map.toSeq
            |> Seq.map snd
    
    let CurrentLogger<'T>() = Logger<'T>(writers)
    let Logger name = Logger(name, writers)
