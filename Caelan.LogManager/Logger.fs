namespace Caelan.LogManager

type Logger internal (name) =
    member __.Log(logType, message) =
        match (logType) with
        | LogType.Debug -> ignore
        | LogType.Trace -> ignore
        | LogType.Warning -> ignore
        | LogType.Error -> ignore
        | LogType.Fatal -> ignore

type Logger<'T> internal () =
    inherit Logger(typeof<'T>.GetType().Name)