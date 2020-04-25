﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Rush.Objects;
using osu.Game.Rulesets.Rush.Objects.Drawables;
using osu.Game.Rulesets.Rush.Replays;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Rush.UI
{
    [Cached]
    public class DrawableRushRuleset : DrawableScrollingRuleset<RushHitObject>
    {
        protected override bool UserScrollSpeedAdjustment => false;

        protected override ScrollVisualisationMethod VisualisationMethod => ScrollVisualisationMethod.Constant;

        public DrawableRushRuleset(RushRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 750;
        }

        protected override Playfield CreatePlayfield() => new RushPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new RushFramedReplayInputHandler(replay);

        public override DrawableHitObject<RushHitObject> CreateDrawableRepresentation(RushHitObject h)
        {
            switch (h)
            {
                case Minion minion:
                    return new DrawableMinion(minion);

                case MiniBoss miniBoss:
                    return new DrawableMiniBoss(miniBoss);

                case NoteSheet noteSheet:
                    return new DrawableNoteSheet(noteSheet);

                case DualOrb dualOrb:
                    return new DrawableDualOrb(dualOrb);
            }

            return null;
        }

        protected override PassThroughInputManager CreateInputManager() => new RushInputManager(Ruleset?.RulesetInfo);
    }
}