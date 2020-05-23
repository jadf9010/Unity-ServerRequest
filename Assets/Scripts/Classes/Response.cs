using UnityEngine;
using System.Collections;

public class Response<T> 
{
    public bool OK;
    public string error;
    public T user;
}
