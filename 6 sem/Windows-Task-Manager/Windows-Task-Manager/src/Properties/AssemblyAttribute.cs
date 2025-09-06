[AttributeUsage(AttributeTargets.Assembly)]
sealed class GitCommitAttribute : Attribute {
    public string GitCommit { get; }
    public GitCommitAttribute(string GitCommit) { this.GitCommit = GitCommit; }
}

[AttributeUsage(AttributeTargets.Assembly)]
sealed class BuildMarkAttribute : Attribute {
    public string BuildMark { get; }
    public BuildMarkAttribute(string BuildMark) { this.BuildMark = BuildMark; }
}
