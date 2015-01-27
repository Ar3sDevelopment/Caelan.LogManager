namespace Caelan.LogManager

open System.Collections.Generic
open System.Configuration
open System.Reflection
open System

[<AbstractClass>]
[<Sealed>]
type Log() = 
    
    static let conf = 
        match ConfigurationManager.GetSection("log") :?> LogConfiguration with
        | null -> None
        | t -> Some(t)
    
    static let confWriters = 
        match conf with
        | None -> None
        | Some(t) when t.Writers.Count < 1 -> None
        | Some(t) -> Some(t.Writers)
    
    static let writersTypes = Dictionary<string, Type>()
    static do [ WriterType.File.ToString(), typeof<FileLogWriter> ] |> Seq.iter writersTypes.Add
    static let mutable writers = Seq.empty<ILogWriter>
    
    static let maxLevel = 
        match conf with
        | None -> LogType.Error
        | Some(t) -> LogType.FromString(t.MaxLevel)
    
    static member Refresh() = 
        writers <- match confWriters with
                   | None -> Seq.empty<ILogWriter>
                   | Some(ws) -> 
                       writersTypes
                       |> Seq.filter (fun item -> ws.Item(item.Key) <> null)
                       |> Seq.map (fun item -> 
                              let res = Activator.CreateInstance(item.Value) :?> ILogWriter
                              res.Assign(ws.Item(item.Key))
                              res)
    
    static member AddWriter name t = 
        writersTypes.Add(name, t)
        Log.Refresh()
    
    static member CurrentLogger<'T>() = Logger<'T>(maxLevel, writers)
    static member CurrentLogger name = Logger(name, maxLevel, writers)
