using System.Linq;
using Content.Shared.Store;
using Content.Shared.Store.Components;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;

namespace Content.Server.Backmen.Vampiric;

[MeansImplicitUse]
public sealed partial class ListingCondition
{
    [DataField("parent", required: true)]
    public ProtoId<ListingPrototype> ParentId;

}
