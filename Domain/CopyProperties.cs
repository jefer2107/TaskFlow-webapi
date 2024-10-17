namespace Domain;

public class CopyProperties<T>
{
    public static void Set(T model, T modelToBeUpdated)
    {
        var modelProperties = typeof(T).GetProperties();

        foreach(var property in modelProperties)
        {
            var value = property.GetValue(model);

            if(value != null)
                property.SetValue(modelToBeUpdated, value);
        }
    }
}
