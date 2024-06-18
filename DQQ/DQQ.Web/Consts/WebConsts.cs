namespace DQQ.Consts
{
  public static class WebConsts
  {
    public static string? URL { get; set; }
    public static string DocumentURL => $"{URL}swagger";
    public const string CoCosURL = "cocos/index.html";

    public const string ActiveSkillChooseText = "主动技能选择";
		public const string SupportSkillChooseText = "辅助技能选择";
		public const string StrategySkillChooseText = "技能策略选择";

    public const string CombatStyleTypeKey = "combat_styletype_key";
	}
}
