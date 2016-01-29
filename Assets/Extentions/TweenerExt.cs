using System.Linq;
using DG.Tweening;
using UnityEngine;

public static class TweenerExt
{
    public static Sequence JoinIntoSequence(this Tweener tweener, params Tweener[] weeners)
    {
        return weeners.Aggregate(DOTween.Sequence().Join(tweener), (seq, tween) => seq.Join(tween));
    }
}
