namespace Caelan.LogManager

open System
open System.Configuration

[<AllowNullLiteral>]
type WriterElement() = 
    inherit ConfigurationElement()
    
    [<ConfigurationProperty("name", IsKey = true, IsRequired = true)>]
    member t.Name = string (t.["name"])
    
    [<ConfigurationProperty("source", IsKey = true, IsRequired = false)>]
    member t.Source = string (t.["source"])
    
    [<ConfigurationProperty("format", IsKey = true, IsRequired = false)>]
    member t.Format = string (t.["format"])

[<AllowNullLiteral>]
type WriterCollection() = 
    inherit ConfigurationElementCollection()
    override __.CreateNewElement() = WriterElement() :> ConfigurationElement
    override __.GetElementKey(element) = box (element :?> WriterElement).Name
    override __.ElementName = "writer"
    override __.IsElementName(name) = not (String.IsNullOrEmpty(name)) && name = "writer"
    override __.CollectionType = ConfigurationElementCollectionType.BasicMap
    member this.Item(i : int) = this.BaseGet(i) :?> WriterElement
    member this.Item(key : string) = this.BaseGet(key) :?> WriterElement

[<AllowNullLiteral>]
type LogConfiguration() = 
    inherit ConfigurationSection()
    
    [<ConfigurationProperty("maxLevel", IsKey = true, IsRequired = false)>]
    member __.MaxLevel 
        with get () = ``base``.["maxLevel"] :?> string
        and set (v : string) = ``base``.["maxLevel"] <- v
    
    [<ConfigurationProperty("writers", IsDefaultCollection = true, IsKey = true, IsRequired = true)>]
    member __.Writers 
        with get () = ``base``.["writers"] :?> WriterCollection
        and set (v : WriterCollection) = ``base``.["writers"] <- v
