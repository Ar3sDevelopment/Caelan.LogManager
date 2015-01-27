namespace Caelan.LogManager

type Logger internal (name, maxLevel, writers : seq<ILogWriter>) = 
    member val MaxLevel = maxLevel
    
    member __.Log logType message = 
        if logType >= maxLevel then 
            for writer in writers do
                (logType, message) ||> writer.Log
    
    member __.LogException logType message ex = 
        if logType >= maxLevel then 
            for writer in writers do
                (logType, message, ex) |||> writer.LogException

type Logger<'T> internal (maxLevel, writers) = 
    inherit Logger(typeof<'T>.GetType().Name, maxLevel, writers)
