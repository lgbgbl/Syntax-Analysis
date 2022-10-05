# 基于Winform实现的语法分析器

### 支持BNF范式表示的上下文无关文法（II型文法）
**示例文法如下 （产生式右部的符号之间用空格分隔，Epsilon用~表示）**
```
E' -> E
E -> E + T | T
T -> T * F | F
F -> ( E ) | i
```

### 主要功能
##### 查看各非终结符的First集合、Follow集合 
##### 输入句子使用LL或LR分析技术对句子进行分析，并使用表格展示分析的全过程
##### 查看DFA
* LR(0)DFA
* LR(1)DFA
* LALR(1)DFA
##### 查看分析表
* LL(1)分析表
* LR(0)分析表
* SLR(1)分析表
* LR(1)分析表
* LALR(1)分析表
##### 其他功能说明
* 查看LL(1)分析表时会自动检查文法是否包含左递归或左公因子
* 查看分析表时会进行二义性检查（结合以上两点即可以做到文法的判别）
* 进行句子分析时，会先自动生成并展示相应的分析表
* 句子分析中输入自定义句子时，无需考虑空格回车之类字符的影响，只需使输入的句子符合相应的文法规则即可

### 项目目录结构
```
.
├── Analyze
│   ├── Analyze.cs
│   ├── LLAnalyze.cs
│   └── LRAnalyze.cs
├── DFA
│   ├── BaseDFA.cs
│   ├── LALR1DFA.cs
│   ├── LR0DFA.cs
│   └── LR1DFA.cs
├── FirstAndFollow
│   ├── GenerateFirst.cs
│   └── GenerateFollow.cs
├── Form
│   ├── FormDFADictionary.Designer.cs
│   ├── FormDFADictionary.cs
│   ├── FormDFADictionary.resx
│   ├── FormFirstAndFollow.Designer.cs
│   ├── FormFirstAndFollow.cs
│   ├── FormFirstAndFollow.resx
│   ├── FormInputSentence.Designer.cs
│   ├── FormInputSentence.cs
│   ├── FormInputSentence.resx
│   ├── FormSyntaxAnalysis.Designer.cs
│   ├── FormSyntaxAnalysis.cs
│   └── FormSyntaxAnalysis.resx
├── InputGrammer.cs
├── Production
│   ├── Production.cs
│   ├── ProductionInLR0.cs
│   └── ProductionInLR1.cs
├── Program.cs
├── Properties
│   ├── AssemblyInfo.cs
│   ├── Resources.Designer.cs
│   ├── Resources.resx
│   ├── Settings.Designer.cs
│   └── Settings.settings
├── PublicFunc.cs
├── SyntaxAnalysis.csproj
├── SyntaxAnalysis.csproj.user
├── SyntaxAnalysis.sln
├── Table
│   ├── LALR1Table.cs
│   ├── LL1Table.cs
│   ├── LR0Table.cs
│   ├── LR1Table.cs
│   ├── LRTable.cs
│   ├── SLR1Table.cs
│   └── Table.cs
└── testdata
    ├── tiny_language.txt
    └── testdata.txt
```

### 其他
* 项目目标框架为 .NET 6.0.1
* 本项目并没有在算法上作过多的优化。因此在应对复杂文法时，特别是在查看关于LR(1)或LALR(1)的DFA或分析表时，速度也许会有点感人，请耐心等待
* 本语法分析器以图形化的形式展现了编译原理语法分析的相关内容，希望能给大家的编译原理学习提供一些帮助