namespace Caelan.LogManager

open System.Collections.Generic

module Log =
    let private writers = List<ILogWriter>()

    let private AddWriter writer = 
        writers.Add(writer)

    let CurrentLogger<'T>() =
        Logger<'T>(writers)

    let Logger name =
        Logger(name, writers)