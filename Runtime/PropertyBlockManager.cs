using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[RequireComponent(typeof(Renderer))]
[ExecuteInEditMode]
public class PropertyBlockManager : MonoBehaviour
{
    [SerializeField]
    private bool affectChildren = true;

    [SerializeField]
    private List<Pair<string, Color>> colorParams;
    [SerializeField]
    private List<Pair<string, float>> floatParams;
    [SerializeField]
    private List<Pair<string, int>> intParams;
    [SerializeField]
    private List<Pair<string, Vector4>> vectorParams;
    [SerializeField]
    private List<Pair<string, bool>> boolParams;


    private MaterialPropertyBlock propertyBlock = null;

    // Start is called before the first frame update
    void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        PropertyBlockInit(transform);
    }

    private void OnValidate()
    {
        propertyBlock = new MaterialPropertyBlock();
        PropertyBlockInit(transform);
    }

    private void PropertyBlockInit(Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);

            PropertyBlockInitList(colorParams);
            PropertyBlockInitList(floatParams);
            PropertyBlockInitList(intParams);
            PropertyBlockInitList(vectorParams);
            PropertyBlockInitList(boolParams);

            renderer.SetPropertyBlock(propertyBlock);
        }

        if(affectChildren)
        {
            for(int i = 0; i < t.childCount; i++)
            {
                PropertyBlockInit(t.GetChild(i).gameObject.transform);
            }
        }
    }

    private void PropertyBlockInitList(List<Pair<string, Color>> list)
    {
        foreach(Pair<string, Color> elem in list)
        {
            propertyBlock.SetColor(elem.item1, elem.item2);
        }
    }
    private void PropertyBlockInitList(List<Pair<string, float>> list)
    {
        foreach (Pair<string, float> elem in list)
        {
            propertyBlock.SetFloat(elem.item1, elem.item2);
        }
    }
    private void PropertyBlockInitList(List<Pair<string, int>> list)
    {
        foreach (Pair<string, int> elem in list)
        {
            propertyBlock.SetInt(elem.item1, elem.item2);
        }
    }
    private void PropertyBlockInitList(List<Pair<string, Vector4>> list)
    {
        foreach (Pair<string, Vector4> elem in list)
        {
            propertyBlock.SetVector(elem.item1, elem.item2);
        }
    }
    private void PropertyBlockInitList(List<Pair<string, bool>> list)
    {
        foreach (Pair<string, bool> elem in list)
        {
            propertyBlock.SetFloat(elem.item1, (elem.item2)?1f:0f);
        }
    }

    public void SetProperty(Pair<string, Color> property, Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(property.item1, property.item2);
            renderer.SetPropertyBlock(propertyBlock);
        }

        if (affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                PropertyBlockManager propertyBlockManager;
                if (t.GetChild(i).TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
                {
                    propertyBlockManager.SetProperty(property, t.GetChild(i));
                }
                else
                {
                    SetProperty(property, t.GetChild(i));
                }
            }
        }
    }
    public void SetProperty(Pair<string, float> property, Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(property.item1, property.item2);
            renderer.SetPropertyBlock(propertyBlock);
        }

        if (affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                PropertyBlockManager propertyBlockManager;
                if (t.GetChild(i).TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
                {
                    propertyBlockManager.SetProperty(property, t.GetChild(i));
                } else
                {
                    SetProperty(property, t.GetChild(i));
                }
            }
        }
    }
    public void SetProperty(Pair<string, int> property, Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetInt(property.item1, property.item2);
            renderer.SetPropertyBlock(propertyBlock);
        }

        if (affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                PropertyBlockManager propertyBlockManager;
                if (t.GetChild(i).TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
                {
                    propertyBlockManager.SetProperty(property, t.GetChild(i));
                }
                else
                {
                    SetProperty(property, t.GetChild(i));
                }
            }
        }
    }
    public void SetProperty(Pair<string, Vector4> property, Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetVector(property.item1, property.item2);
            renderer.SetPropertyBlock(propertyBlock);
        }

        if (affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                PropertyBlockManager propertyBlockManager;
                if (t.GetChild(i).TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
                {
                    propertyBlockManager.SetProperty(property, t.GetChild(i));
                }
                else
                {
                    SetProperty(property, t.GetChild(i));
                }
            }
        }
    }
    public void SetProperty(Pair<string, bool> property, Transform t)
    {
        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(property.item1, (property.item2) ? 1f : 0f);
            renderer.SetPropertyBlock(propertyBlock);
        }

        if (affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                PropertyBlockManager propertyBlockManager;
                if (t.GetChild(i).TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
                {
                    propertyBlockManager.SetProperty(property, t.GetChild(i));
                }
                else
                {
                    SetProperty(property, t.GetChild(i));
                }
            }
        }
    }



    static public void SetProperty(Pair<string, Color> property, Transform t, bool _affectChildren)
    {
        PropertyBlockManager propertyBlockManager;
        if(t.TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
        {
            propertyBlockManager.SetProperty(property, t);
            return;
        }

        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(property.item1, property.item2);
            renderer.SetPropertyBlock(_propertyBlock);
        }

        if (_affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                SetProperty(property, t.GetChild(i), _affectChildren);
            }
        }
    }
    static public void SetProperty(Pair<string, float> property, Transform t, bool _affectChildren)
    {
        PropertyBlockManager propertyBlockManager;
        if (t.TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
        {
            propertyBlockManager.SetProperty(property, t);
            return;
        }

        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(property.item1, property.item2);
            renderer.SetPropertyBlock(_propertyBlock);
        }

        if (_affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                SetProperty(property, t.GetChild(i), _affectChildren);
            }
        }
    }
    static public void SetProperty(Pair<string, int> property, Transform t, bool _affectChildren)
    {
        PropertyBlockManager propertyBlockManager;
        if (t.TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
        {
            propertyBlockManager.SetProperty(property, t);
            return;
        }

        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetInt(property.item1, property.item2);
            renderer.SetPropertyBlock(_propertyBlock);
        }

        if (_affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                SetProperty(property, t.GetChild(i), _affectChildren);
            }
        }
    }
    static public void SetProperty(Pair<string, Vector4> property, Transform t, bool _affectChildren)
    {
        PropertyBlockManager propertyBlockManager;
        if (t.TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
        {
            propertyBlockManager.SetProperty(property, t);
            return;
        }

        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetVector(property.item1, property.item2);
            renderer.SetPropertyBlock(_propertyBlock);
        }

        if (_affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                SetProperty(property, t.GetChild(i), _affectChildren);
            }
        }
    }
    static public void SetProperty(Pair<string, bool> property, Transform t, bool _affectChildren)
    {
        PropertyBlockManager propertyBlockManager;
        if (t.TryGetComponent<PropertyBlockManager>(out propertyBlockManager))
        {
            propertyBlockManager.SetProperty(property, t);
            return;
        }

        Renderer renderer;
        if (t.TryGetComponent<Renderer>(out renderer))
        {
            MaterialPropertyBlock _propertyBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(property.item1, (property.item2) ? 1f : 0f);
            renderer.SetPropertyBlock(_propertyBlock);
        }

        if (_affectChildren)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                SetProperty(property, t.GetChild(i), _affectChildren);
            }
        }
    }
}

[System.Serializable]
public class Pair<X, Y>
{
    public X item1;
    public Y item2;

    public Pair(X _x, Y _y)
    {
        item1 = _x;
        item2 = _y;
    }
}