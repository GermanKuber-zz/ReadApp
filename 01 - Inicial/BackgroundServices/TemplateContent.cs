namespace BackgroundServices
{
    public static class TemplateContent
    {
        public static string WithImage => @"<tile>
                                                <visual branding=""name"">
                                                    <binding template=""TileMedium"">
                                                        <text hint-style=""caption"">{0}</text>
                                                        <text hint-style=""captionSubtle"" hint-wrap=""true"">{1}</text>
                                                    </binding>
                                                <binding template=""TileWide"">
                                                    <group>
                                                    <subgroup hint-weight=""33"">
                                                        <image src=""{2}"" />
                                                    </subgroup>
                                                    <subgroup>
                                                        <text hint-style=""caption"">{0}</text>
                                                        <text hint-style=""captionSubtle"" hint-wrap=""true"" hint-maxLines=""3"">{1}</text>
                                                    </subgroup>
                                                    </group>
                                                </binding>
                                                <binding template=""TileLarge"">
                                                    <group>
                                                    <subgroup hint-weight=""33"">
                                                        <image src=""{2}"" />
                                                    </subgroup>
                                                    <subgroup>
                                                        <text hint-style=""caption"">{0}</text>
                                                        <text hint-style=""captionSubtle"" hint-wrap=""true"" hint-maxLines=""3"">263 Grove St, San Francisco, CA 94102</text>
                                                    </subgroup>
                                                    </group>
                                                    <image src=""{2}""/>      
                                                </binding>
                                                </visual>
                                            </tile>";

        public static string Simple=> @"<tile>
                                          <visual version=""4"">
                                            <binding template=""TileMedium"">
	                                          <text hint-wrap=""true"">{0}</text>
	                                        </binding>
                                            <binding template=""TileWide"">
	                                          <text hint-wrap=""true"">{0}</text>
	                                        </binding>
                                          </visual>
                                        </tile>";

    }
}