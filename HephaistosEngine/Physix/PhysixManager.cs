﻿using System.Numerics;
using SFML.Graphics;
namespace Physix;

public class PhysixManager
{
    public List<PhysixObject> _objects;
    

    public Vector3 _gravity;

    public PhysixManager(Vector3 gravity)
    {
        _objects = new List<PhysixObject>();
        _gravity = gravity;
    }
    
    
    public void AddObject(PhysixObject obj)
    {
        obj.Id = _objects.Count;
        _objects.Add(obj);
    }
    

    public void Remove(int id) => _objects.RemoveAll(x => x.Id == id);


    public void Show()
    {
        _objects.ForEach(x => Console.WriteLine($"Id : {x.Id}, Name : {x.Name}, Position : {x.Pos}"));
    }

    public void Update(float delta)
    {
        foreach (PhysixObject obj in _objects)
        {
            obj.ApplyForces(_gravity,delta);
            obj.Update(delta);
        }

        for (int i = 0; i < _objects.Count; i++)
        {
            for (int j = i+1; j < _objects.Count; j++)
            {
                PhysixObject obj1 = _objects[i];
                PhysixObject obj2 = _objects[j];

                if (obj1.Iscolliding(obj2))
                {
                    obj1.Collision(obj2);
                    obj1.Update(delta);
                    obj2.Update(delta);
                }
            }
        }
        
        
    }
    


}