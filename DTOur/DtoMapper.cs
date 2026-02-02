namespace DTOur;

using System.Reflection;
using DTOur.Attributes;
public class DtoMapper<TSource>
{

    public List<TDestination> MapList<TDestination>(List<TSource> sourceList)
    {
        var destinationList = new List<TDestination>(sourceList.Count);
        MapList(sourceList, destinationList);
        return destinationList;
    }

    public List<TDestination> MapList<TDestination>(List<TSource> sourceList, List<TDestination> destinationList)
    {

        for (int i = 0; i < sourceList.Count; i++)
        {
            var source = sourceList[i];
            var destination = destinationList[i] ?? Activator.CreateInstance<TDestination>();
            destination = Map(source, destination);
            destinationList[i] = destination;
        }

        return destinationList;
    }



    public TDestination Map<TDestination>(TSource source)
    {
        var destination = Activator.CreateInstance<TDestination>();
        Map(source, destination);
        return destination;

    }

    public TDestination Map<TDestination>(TSource source, TDestination destination)
    {
        foreach (var prop in typeof(TSource).GetProperties())
        {
            var destProp = typeof(TDestination).GetProperty(prop.Name);
            if (destProp != null && destProp.CanWrite)
            {
                destProp.SetValue(destination, prop.GetValue(source));
            }
        }
        return destination;
    }


    public TSource MapBack<TDestination>(TDestination destination)
    {
        var source = Activator.CreateInstance<TSource>();
        MapBack(destination, source);
        return source;
    }

    public TSource MapBack<TDestination>(TDestination destination, TSource source)
    {

        foreach (var prop in typeof(TDestination).GetProperties())
        {
            var propName = prop.GetCustomAttribute<MapToAttribute>()?.Name ?? prop.Name;

            var sourceProp = typeof(TSource).GetProperty(propName);

            if (sourceProp != null && sourceProp.CanWrite)
            {
                sourceProp.SetValue(source, prop.GetValue(destination));
            }
        }
        return source;
    }

    public List<TSource> MapBackList<TDestination>(List<TDestination> destinationList)
    {
        var sourceList = new List<TSource>(destinationList.Count);
        MapBackList(destinationList, sourceList);
        return sourceList;
    }

    /// <summary>
    /// Method <c>Draw</c> renders the point.
    /// </summary>
    public List<TSource> MapBackList<TDestination>(List<TDestination> destinationList, List<TSource> sourceList)
    {

        for (int i = 0; i < destinationList.Count; i++)
        {
            var destination = destinationList[i];
            var source = sourceList[i] ?? Activator.CreateInstance<TSource>();
            source = MapBack(destination, source);
            sourceList[i] = source;
        }
        return sourceList;
    }

}
