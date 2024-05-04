﻿using System;
using System.Collections;
using System.Collections.Generic;
using SabberStoneCore.Kettle;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Model.Zones;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    public List<GameObject> Entities;

    // Start is called before the first frame update
    void Start()
    {
        Entities = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    internal void Add(GameObject entity)
    {
        entity.transform.SetParent(transform, false);
        Entities.Add(entity);

        if (entity.TryGetComponent(out DraggableCard drag))
        {
            drag.CacheParent(transform);
        }
    }

    internal void Remove(GameObject entity)
    {
        Entities.Remove(entity);
        if (entity.TryGetComponent(out DraggableCard drag))
        {
            drag.CacheParent(null);
        }
    }

    internal void Order()
    {
        if (Entities.Count < 2)
        {
            return;
        }

        var basicGen = Entities[0].GetComponent<BasicGen>();
        if (basicGen != null)
        {
            Entities.Sort((a, b) => 
            a.GetComponent<BasicGen>().Tag(SabberStoneCore.Enums.GameTag.ZONE_POSITION)
            .CompareTo(b.GetComponent<BasicGen>().Tag(SabberStoneCore.Enums.GameTag.ZONE_POSITION)));
            foreach(var card in Entities)
            {
                card.transform.SetAsLastSibling();
            }
        }
    }

    internal void Clear()
    {
        Entities.ForEach(p => {
            Destroy(p);
        });
        Entities.Clear();
    }
}
