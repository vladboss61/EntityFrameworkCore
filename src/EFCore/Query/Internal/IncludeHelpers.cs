// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    public static class IncludeHelpers
    {
        public static void CopyIncludeInformation(NavigationTreeNode originalNavigationTree, NavigationTreeNode newNavigationTree, SourceMapping newSourceMapping)
        {
            foreach (var child in originalNavigationTree.Children.Where(n => n.Included == NavigationTreeNodeIncludeMode.ReferencePending || n.Included == NavigationTreeNodeIncludeMode.Collection))
            {
                var copy = NavigationTreeNode.Create(newSourceMapping, child.Navigation, newNavigationTree, true);
                CopyIncludeInformation(child, copy, newSourceMapping);
            }
        }
    }
}
