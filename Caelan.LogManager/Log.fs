namespace Caelan.LogManager

module Log =
    let CurrentLogger<'T>() =
        Logger<'T>()

    let Logger name =
        Logger(name)