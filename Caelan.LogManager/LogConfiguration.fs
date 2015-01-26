namespace Caelan.LogManager

open System
open System.Configuration

[<AllowNullLiteral>]
type FileWriterElement() =
    inherit ConfigurationElement()

    [<ConfigurationProperty("path", IsKey = true, IsRequired = true)>]
    member t.Path with get() = string(t.["path"])

[<AllowNullLiteral>]
type LogConfiguration() = 
    inherit ConfigurationSection()

    [<ConfigurationProperty("fileWriter", IsKey = true)>]
    member __.FileWriter
        with get() = base.["fileWriter"] :?> FileWriterElement
        and set (v : FileWriterElement) = base.["fileWriter"] <- v