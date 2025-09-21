namespace MesApp.Domain;

public enum ItemType { RAW, SEMI, FG }
public enum ReceiptStatus { New, OnQc, OnLab, Accepted, Rejected }
public enum QcStage { Incoming, InProcess, Final }
public enum Decision { Allow, Reject, Rework }
public enum PrepKind { StockAnalysis, Nesting, HeatTreat, PreTests, TurningForUT }
public enum PrepStatus { Planned, InProgress, Done, Blocked }
public enum WoStatus { New, MaterialRequired, MaterialReady, InProgress, QcHold, ReadyToShip, Completed }