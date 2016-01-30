using System.Collections.Generic;
using System.Linq;

public class CardReducer
{
    public struct Resolution
    {
        public int PlayerDamageTaken;
        public int OpponentDamageTaken;
    }

    public Resolution ResolveCard (CardDescriptor playerCard, CardDescriptor opponentCard, SkirmishModifiers skirmishModifiers)
    {
        var player = FlattenAttr (playerCard.CardAttributes);
        var opponent = FlattenAttr (opponentCard.CardAttributes);

        return new Resolution {
            OpponentDamageTaken = skirmishModifiers.CalculateDamageTaken (ModifierTarget.Oponnnent, player, opponent),
            PlayerDamageTaken = skirmishModifiers.CalculateDamageTaken (ModifierTarget.Player, opponent, player),
        };
    }

    private List<CardAttribute.Type> FlattenAttr (List<CardAttribute> attributes)
    {
        return attributes.SelectMany (s => Enumerable.Range (1, s.Quantity).Select (i => s.Quality)).ToList ();
    }
}
