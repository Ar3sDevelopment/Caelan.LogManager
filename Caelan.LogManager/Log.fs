namespace Caelan.LogManager

open System.Collections.Generic
open System.Configuration
open System.Reflection
open System

module Log = 
    let private writersTypes = Dictionary<string, Type>()
    
    [ WriterType.File.ToString(), typeof<FileLogWriter> ] |> Seq.iter writersTypes.Add
    
    //        Assembly.GetExecutingAssembly().GetTypes() 
    //        |> Seq.filter (fun t -> typeof<ILogW  riter>.IsAssignableFrom(t) && not t.IsInterface)
    let private conf = ConfigurationManager.GetSection("log") :?> LogConfiguration
    
    let private confWriters = 
        match conf with
        | null -> None
        | t when t.Writers.Count < 1 -> None
        | _ -> Some(conf.Writers)
    
    let private Writers() = 
        match confWriters with
        | None -> Seq.empty<ILogWriter>
        | Some(ws) -> 
            writersTypes
            |> Seq.filter (fun item -> ws.Item(item.Key) <> null)
            |> Seq.map (fun item -> 
                   let res = Activator.CreateInstance(item.Value) :?> ILogWriter
                   res.Assign(ws.Item(item.Key))
                   res)
    
    let AddWriter name t = writersTypes.Add(name, t)
    let CurrentLogger<'T>() = Logger<'T>(Writers())
    let Logger name = Logger(name, Writers())
