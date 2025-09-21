using MesApp.Domain;

namespace MesApp.Data;

public static class SeedData
{
    public static void EnsureSeed(AppDbContext db)
    {
        // Warehouses
        if (!db.Warehouses.Any())
        {
            db.Warehouses.AddRange(
                new Warehouse { Name = "QUAR", Kind = "QUARANTINE" },
                new Warehouse { Name = "RAW", Kind = "RAW" },
                new Warehouse { Name = "INT", Kind = "INTERMEDIATE" },
                new Warehouse { Name = "FG", Kind = "FG" }
            );
        }

        // Items - расширенный список материалов
        if (!db.Items.Any())
        {
            db.Items.AddRange(
                // RAW материалы
                new Item { Name = "Лист стальной 09Г2С 20мм", Type = ItemType.RAW, Unit = "кг", Spec = "ГОСТ 19903-2015" },
                new Item { Name = "Лист стальной 12Х18Н10Т 10мм", Type = ItemType.RAW, Unit = "кг", Spec = "ГОСТ 7350-77" },
                new Item { Name = "Труба 325x12 09Г2С", Type = ItemType.RAW, Unit = "м", Spec = "ГОСТ 20295-85" },
                new Item { Name = "Электроды МР-3 ø4мм", Type = ItemType.RAW, Unit = "кг", Spec = "ГОСТ 9466-75" },
                new Item { Name = "Проволока СВ-08Г2С ø1.2мм", Type = ItemType.RAW, Unit = "кг", Spec = "ГОСТ 2246-70" },

                // SEMI полуфабрикаты
                new Item { Name = "Заготовка фланца Ø300 PN16", Type = ItemType.SEMI, Unit = "шт", Spec = "СТО-001" },
                new Item { Name = "Заготовка днища эллиптического Ø1000", Type = ItemType.SEMI, Unit = "шт", Spec = "СТО-002" },
                new Item { Name = "Цилиндрическая обечайка Ø800", Type = ItemType.SEMI, Unit = "шт", Spec = "СТО-003" },

                // FG готовая продукция
                new Item { Name = "Фланец приварной Ø300 PN16", Type = ItemType.FG, Unit = "шт", Spec = "ГОСТ 12821-80" },
                new Item { Name = "Днище эллиптическое Ø1000x12", Type = ItemType.FG, Unit = "шт", Spec = "ГОСТ 6533-78" },
                new Item { Name = "Емкость технологическая V=2м³", Type = ItemType.FG, Unit = "шт", Spec = "ТУ 3612-001" }
            );
        }

        // Business Partners - расширенный список
        if (!db.BusinessPartners.Any())
        {
            db.BusinessPartners.AddRange(
                // Поставщики
                new BusinessPartner { Name = "ММК-МЕТИЗ", Role = "SUPPLIER" },
                new BusinessPartner { Name = "Северсталь-Метиз", Role = "SUPPLIER" },
                new BusinessPartner { Name = "ЧТПЗ", Role = "SUPPLIER" },
                new BusinessPartner { Name = "Уралтрубпром", Role = "SUPPLIER" },
                new BusinessPartner { Name = "СпецЭлектрод", Role = "SUPPLIER" },

                // Заказчики
                new BusinessPartner { Name = "Газпром Нефтехим", Role = "CUSTOMER" },
                new BusinessPartner { Name = "ТАИФ-НК", Role = "CUSTOMER" },
                new BusinessPartner { Name = "Роснефть-ЯНОС", Role = "CUSTOMER" },
                new BusinessPartner { Name = "Лукойл-НОРСИ", Role = "CUSTOMER" }
            );
        }

        db.SaveChanges();

        // Тестовые приёмки для демонстрации workflow
        if (!db.MaterialReceipts.Any())
        {
            var steel09g2s = db.Items.First(i => i.Name.Contains("09Г2С 20мм"));
            var steel12h18 = db.Items.First(i => i.Name.Contains("12Х18Н10Т"));
            var tube325 = db.Items.First(i => i.Name.Contains("Труба 325"));
            var electrodes = db.Items.First(i => i.Name.Contains("Электроды"));

            var mmkMetiz = db.BusinessPartners.First(b => b.Name == "ММК-МЕТИЗ");
            var severstal = db.BusinessPartners.First(b => b.Name == "Северсталь-Метиз");
            var chTPZ = db.BusinessPartners.First(b => b.Name == "ЧТПЗ");

            var receipts = new List<MaterialReceipt>
            {
                // Приёмка 1 - Новая (для тестирования полного цикла)
                new MaterialReceipt
                {
                    ItemId = steel09g2s.Id,
                    SupplierId = mmkMetiz.Id,
                    Qty = 1250.5m,
                    Unit = "кг",
                    Grade = "09Г2С",
                    Size = "20x1500x6000",
                    HeatNumber = "H240915001",
                    CertNumber = "C240915-001",
                    Status = ReceiptStatus.New,
                    ReceivedAt = DateTime.Now.AddDays(-2),
                    CreatedBy = "Анна"
                },

                // Приёмка 2 - На контроле ОТК
                new MaterialReceipt
                {
                    ItemId = steel12h18.Id,
                    SupplierId = severstal.Id,
                    Qty = 890.0m,
                    Unit = "кг",
                    Grade = "12Х18Н10Т",
                    Size = "10x1250x2500",
                    HeatNumber = "H240910002",
                    CertNumber = "C240910-002",
                    Status = ReceiptStatus.OnQc,
                    ReceivedAt = DateTime.Now.AddDays(-3),
                    CreatedBy = "Анна"
                },

                // Приёмка 3 - В лаборатории
                new MaterialReceipt
                {
                    ItemId = tube325.Id,
                    SupplierId = chTPZ.Id,
                    Qty = 18.5m,
                    Unit = "м",
                    Grade = "09Г2С",
                    Size = "325x12",
                    HeatNumber = "H240905003",
                    CertNumber = "C240905-003",
                    Status = ReceiptStatus.OnLab,
                    ReceivedAt = DateTime.Now.AddDays(-5),
                    CreatedBy = "Анна"
                },

                // Приёмка 4 - Принято (доступно для выдачи)
                new MaterialReceipt
                {
                    ItemId = electrodes.Id,
                    SupplierId = db.BusinessPartners.First(b => b.Name == "СпецЭлектрод").Id,
                    Qty = 50.0m,
                    Unit = "кг",
                    AllocatedQty = 15.0m, // Частично выдано
                    Grade = "МР-3",
                    Size = "ø4мм",
                    HeatNumber = "H240901004",
                    CertNumber = "C240901-004",
                    Status = ReceiptStatus.Accepted,
                    ReceivedAt = DateTime.Now.AddDays(-7),
                    CreatedBy = "Анна"
                },

                // Приёмка 5 - Отклонено (для демонстрации)
                new MaterialReceipt
                {
                    ItemId = steel09g2s.Id,
                    SupplierId = mmkMetiz.Id,
                    Qty = 500.0m,
                    Unit = "кг",
                    Grade = "09Г2С",
                    Size = "20x1500x6000",
                    HeatNumber = "H240820005",
                    CertNumber = "C240820-005",
                    Status = ReceiptStatus.Rejected,
                    ReceivedAt = DateTime.Now.AddDays(-10),
                    CreatedBy = "Анна"
                }
            };

            db.MaterialReceipts.AddRange(receipts);
            db.SaveChanges();

            // QC Inspections для демонстрации
            var onQcReceipt = receipts.First(r => r.Status == ReceiptStatus.OnQc);
            var rejectedReceipt = receipts.First(r => r.Status == ReceiptStatus.Rejected);

            db.QcInspections.AddRange(
                new QcInspection
                {
                    MaterialReceiptId = rejectedReceipt.Id,
                    Stage = QcStage.Incoming,
                    RequiresUltrasonic = false,
                    RequiresPpsd = true,
                    FinalDecision = Decision.Reject,
                    Remarks = "Несоответствие размеров чертежу. Отклонение толщины +2мм.",
                    CreatedAt = DateTime.Now.AddDays(-9),
                    Inspector = "Пётр"
                }
            );

            // Lab Test Requests для демонстрации
            var onLabReceipt = receipts.First(r => r.Status == ReceiptStatus.OnLab);
            var acceptedReceipt = receipts.First(r => r.Status == ReceiptStatus.Accepted && r.ItemId == electrodes.Id);

            var labRequests = new List<LabTestRequest>
            {
                new LabTestRequest
                {
                    MaterialReceiptId = onLabReceipt.Id,
                    External = false,
                    Tests = "CHEM,HARDNESS,UT",
                    Status = "InProgress",
                    CreatedAt = DateTime.Now.AddDays(-4),
                    RequestedBy = "Пётр"
                },
                new LabTestRequest
                {
                    MaterialReceiptId = acceptedReceipt.Id,
                    External = false,
                    Tests = "CHEM,HARDNESS",
                    Status = "Completed",
                    CreatedAt = DateTime.Now.AddDays(-6),
                    RequestedBy = "Пётр"
                }
            };

            db.LabTestRequests.AddRange(labRequests);

            // Lab Test Results для завершённой заявки - пропускаем, так как нет нужных полей в модели

            // Prep Jobs для демонстрации
            db.PrepJobs.AddRange(
                new PrepJob
                {
                    MaterialReceiptId = acceptedReceipt.Id,
                    Kind = PrepKind.Nesting,
                    Status = PrepStatus.Done,
                    Notes = "Раскрой выполнен согласно карте раскроя №НГ-001",
                    Owner = "Иван",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new PrepJob
                {
                    MaterialReceiptId = onLabReceipt.Id,
                    Kind = PrepKind.StockAnalysis,
                    Status = PrepStatus.InProgress,
                    Notes = "Ожидание результатов лабораторных испытаний",
                    Owner = "Иван",
                    CreatedAt = DateTime.Now.AddDays(-3)
                }
            );

            // Issue to Production записи
            db.IssueToProductions.Add(
                new IssueToProduction
                {
                    ReceiptId = acceptedReceipt.Id,
                    Qty = 15.0m,
                    IssuedAt = DateTime.Now.AddDays(-4),
                    IssuedBy = "Анна"
                }
            );

            db.SaveChanges();
        }
    }
}