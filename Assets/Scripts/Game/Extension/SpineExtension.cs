using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public static class SpineExtension {
    public static void Reload(this SkeletonAnimation skeletonAnimation) {
        if (skeletonAnimation.skeletonDataAsset != null) {
            foreach (var aa in skeletonAnimation.skeletonDataAsset.atlasAssets) {
                if (aa != null) aa.Clear();
            }

            skeletonAnimation.skeletonDataAsset.Clear();
        }

        skeletonAnimation.Initialize(true);
    }

    public static bool IsPlaying(this IAnimationStateComponent skeletonAnimation, string animationName) {
        if (skeletonAnimation == null) return false;
        if (skeletonAnimation.AnimationState?.GetCurrent(0) == null) return false;

        return string.Compare(skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name, animationName,
            StringComparison.Ordinal) == 0;
    }

    public static bool ChangeAnimation(this IAnimationStateComponent skeletonAnimation, string name, bool loop,
        bool restart = false) {
        if (skeletonAnimation.AnimationState == null || (!restart && skeletonAnimation.IsPlaying(name))) return false;
        skeletonAnimation.AnimationState.SetAnimation(0, name, loop);
        return true;
    }

    public static bool EnsureSetAnimation(this IAnimationStateComponent skeletonAnimation, string name, bool loop,
        bool restart = false) {
        if (!restart && skeletonAnimation.IsPlaying(name)) return true;
        skeletonAnimation.ChangeAnimation(name, loop);
        return false;
    }

    public static List<string>
        GetAnimationNamesContainString(this ISkeletonComponent skeletonAnimation, string subStr) {
        var animations = skeletonAnimation.SkeletonDataAsset.GetSkeletonData(false).Animations;
        var list = new List<string>();

        foreach (var animation in animations) {
            if (animation.Name.Contains(subStr)) {
                list.Add(animation.Name);
            }
        }

        return list;
    }

    public static string GetAnimationNameContainString(this SkeletonAnimation skeletonAnimation, string subStr) {
        var animations = skeletonAnimation.skeletonDataAsset.GetSkeletonData(false).Animations;

        foreach (var animation in animations) {
            if (animation.Name.Contains(subStr)) {
                return animation.Name;
            }
        }

        return null;
    }

    public static bool IsCurrentAnimationComplete(this IAnimationStateComponent skeletonAnimation) {
        return skeletonAnimation.AnimationState.GetCurrent(0) == null ||
               skeletonAnimation.AnimationState.GetCurrent(0).IsComplete;
    }

    public static string CurrentAnimationName(this IAnimationStateComponent animationStateComponent) {
        var animation = animationStateComponent.AnimationState.GetCurrent(0);
        return animation != null ? animation.Animation.Name : string.Empty;
    }
}