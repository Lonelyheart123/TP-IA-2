using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomNode : INode
{
    Roulette<INode> _roulette = new Roulette<INode>();
    Dictionary<INode, int> _items;
    public RandomNode(Dictionary<INode, int> items)
    {
        _items = items;
    }

    public void execute()
    {

        var node = _roulette.run(_items);
        if (node != null)
        {
            node.execute();
        }
    }

}
