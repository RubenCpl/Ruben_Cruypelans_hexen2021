using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.Gamesystem
{

    [CreateAssetMenu(menuName = "DAE/CardData")]

    public class CardData : ScriptableObject
    {
        public string _name;
        public string _description;
        public CardType _cardType;
        public Texture2D _cardTexture;
    }
}
