namespace Caelan.LogManager

open System.Collections.Generic
open System.Configuration
open System.Reflection
open System

module Log = 
    let private writersTypes = 
        Assembly.GetExecutingAssembly().GetTypes() 
        |> Seq.filter (fun t -> typeof<ILogWriter>.IsAssignableFrom(t) && not t.IsInterface)
    let private conf = ConfigurationManager.GetSection("log") :?> LogConfiguration
    
    let private confWriters = 
        match conf with
        | null -> None
        | t when t.Writers.Count < 1 -> None
        | _ -> Some(conf.Writers)
    
    let private writers = 
        match confWriters with
        | None -> Seq.empty<ILogWriter>
        | Some(ws) -> writersTypes |> Seq.map (fun t -> Activator.CreateInstance(t, ws.Item(t.Name)) :?> ILogWriter)
    
    let CurrentLogger<'T>() = Logger<'T>(writers)
    let Logger name = Logger(name, writers)
