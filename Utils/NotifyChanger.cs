using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class NotifyChanger : INotifyPropertyChanged
{
    #region Notify methods
    private Dictionary<string, object> dictionary = new Dictionary<string, object>();
    public event PropertyChangedEventHandler PropertyChanged;
    public void Set(object value, [CallerMemberName]String propertyName = null)
    {
        if (string.IsNullOrEmpty(propertyName)) return;

        if (dictionary.ContainsKey(propertyName)) dictionary[propertyName] = value;
        else dictionary.Add(propertyName, value);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public T Get<T>([CallerMemberName]String propertyName = null)
    {
        if (string.IsNullOrEmpty(propertyName)) return default(T);
        if(!dictionary.ContainsKey(propertyName))
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            Set(obj, propertyName);
        }
        return (T)dictionary[propertyName];
    }
    public void Upd([CallerMemberName]String propertyName = null)
    {
        if (string.IsNullOrEmpty(propertyName)) return;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}