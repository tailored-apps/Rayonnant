using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Data;

public class MicroErpDataSeeder
{
    public static void SeedData(MicroErpDbContext context)
    {
        if (context.Customers.Any()) return; // Already seeded

        // Seed lookup data first
        SeedLookupTables(context);
        
        // Then seed customers
        SeedCustomers(context);
        
        // Then PCBs
        SeedPcbs(context);
        
        // Then employees
        SeedEmployees(context);
        
        // Finally orders and guidebooks
        SeedOrders(context);
        SeedGuidebooks(context);

        context.SaveChanges();
    }

    private static void SeedLookupTables(MicroErpDbContext context)
    {
        // Solder Colors
        var solderColors = new[]
        {
            new SolderColor { Id = 1, Name = "Zielona" },
            new SolderColor { Id = 2, Name = "Niebieska" },
            new SolderColor { Id = 3, Name = "Czerwona" },
            new SolderColor { Id = 4, Name = "Biała" },
            new SolderColor { Id = 5, Name = "Czarna" }
        };
        context.SolderColors.AddRange(solderColors);

        // Overlay Colors
        var overlayColors = new[]
        {
            new OverlayColor { Id = 1, Name = "Biała" },
            new OverlayColor { Id = 2, Name = "Czarna" },
            new OverlayColor { Id = 3, Name = "Żółta" }
        };
        context.OverlayColors.AddRange(overlayColors);

        // Materials
        var materials = new[]
        {
            new Material { Id = 1, Name = "FR4", CopperThicknessInUm = "35", MaterialThicknessInMm = 1.6, TwoSided = true },
            new Material { Id = 2, Name = "FR4", CopperThicknessInUm = "70", MaterialThicknessInMm = 1.6, TwoSided = true },
            new Material { Id = 3, Name = "FR4", CopperThicknessInUm = "35", MaterialThicknessInMm = 0.8, TwoSided = true }
        };
        context.Materials.AddRange(materials);

        // Cover Types
        var coverTypes = new[]
        {
            new CoverType { Id = 1, Name = "Bez pokrywy" },
            new CoverType { Id = 2, Name = "HASL" },
            new CoverType { Id = 3, Name = "ENIG" }
        };
        context.CoverTypes.AddRange(coverTypes);

        // Assembly Types
        var assemblyTypes = new[]
        {
            new AssemblyType { Id = 1, Name = "SMT" },
            new AssemblyType { Id = 2, Name = "THT" },
            new AssemblyType { Id = 3, Name = "Mieszana" }
        };
        context.AssemblyTypes.AddRange(assemblyTypes);

        // Assembly Technology Types
        var technologyTypes = new[]
        {
            new AssemblyTechnologyType { Id = 1, Name = "Jednostronna" },
            new AssemblyTechnologyType { Id = 2, Name = "Dwustronna" }
        };
        context.AssemblyTechnologyTypes.AddRange(technologyTypes);

        // Order Types
        var orderTypes = new[]
        {
            new OrderType { Id = 1, Name = "Prototyp" },
            new OrderType { Id = 2, Name = "Seria" },
            new OrderType { Id = 3, Name = "Naprawa" }
        };
        context.OrderTypes.AddRange(orderTypes);

        // Order Delivery Types
        var deliveryTypes = new[]
        {
            new OrderDeliveryType { Id = 1, Name = "Odbiór osobisty" },
            new OrderDeliveryType { Id = 2, Name = "Kurier" },
            new OrderDeliveryType { Id = 3, Name = "Poczta" }
        };
        context.OrderDeliveryTypes.AddRange(deliveryTypes);

        // Components Delivery Types
        var componentDeliveryTypes = new[]
        {
            new ComponentsDeliveryType { Id = 1, Name = "Klient dostarcza" },
            new ComponentsDeliveryType { Id = 2, Name = "Zakup przez nas" }
        };
        context.ComponentsDeliveryTypes.AddRange(componentDeliveryTypes);

        // Documentation Types
        var documentationTypes = new[]
        {
            new DocumentationType { Id = 1, Name = "Gerber" },
            new DocumentationType { Id = 2, Name = "PDF" },
            new DocumentationType { Id = 3, Name = "DXF" }
        };
        context.DocumentationTypes.AddRange(documentationTypes);
    }

    private static void SeedCustomers(MicroErpDbContext context)
    {
        var customers = new[]
        {
            new Customer
            {
                Id = 1,
                Name = "TechnoElektro Sp. z o.o.",
                Address = "ul. Przemysłowa 15",
                City = "Warszawa",
                PostCode = "00-950",
                Person = "Jan Kowalski",
                PhoneNumber = "+48 22 123 45 67",
                Comments = "Stały klient - terminy priorytetowe",
                DefaultDeliveryTypeId = 2
            },
            new Customer
            {
                Id = 2,
                Name = "InnovaTech Kraków",
                Address = "al. Pokoju 8",
                City = "Kraków",
                PostCode = "31-564",
                Person = "Anna Nowak",
                PhoneNumber = "+48 12 987 65 43",
                Comments = "Wymagają certyfikatu jakości",
                DefaultDeliveryTypeId = 1
            },
            new Customer
            {
                Id = 3,
                Name = "ElectroSoft",
                Address = "ul. Gdańska 42",
                City = "Gdańsk",
                PostCode = "80-280",
                Person = "Piotr Wiśniewski",
                PhoneNumber = "+48 58 444 33 22",
                Comments = "Małe serie, szybkie prototypy",
                DefaultDeliveryTypeId = 2
            }
        };
        context.Customers.AddRange(customers);
    }

    private static void SeedPcbs(MicroErpDbContext context)
    {
        var pcbs = new[]
        {
            new Pcb
            {
                Id = 1,
                Name = "Kontroler LED v2.1",
                CustomerId = 1,
                PrototypePrice = 45.50,
                ProductionPrice = 28.30,
                HolePlatting = true,
                Vscoring = true,
                MaterialId = 1,
                DefaultTopSolderId = 1,
                DefaultBottomSolderId = 1,
                DefaultTopOverlayId = 1,
                CoverId = 2,
                Comments = "Wymaga kontroli AOI"
            },
            new Pcb
            {
                Id = 2,
                Name = "Czujnik temperatury v1.0",
                CustomerId = 2,
                PrototypePrice = 23.80,
                ProductionPrice = 15.20,
                HolePlatting = false,
                MaterialId = 3,
                DefaultTopSolderId = 2,
                DefaultTopOverlayId = 2,
                CoverId = 1,
                Comments = "Jednostronna, prostą konstrukcja"
            },
            new Pcb
            {
                Id = 3,
                Name = "Modul WiFi ESP32",
                CustomerId = 3,
                PrototypePrice = 67.90,
                ProductionPrice = 42.10,
                HolePlatting = true,
                CuttingOnEdge = true,
                MaterialId = 1,
                DefaultTopSolderId = 1,
                DefaultBottomSolderId = 1,
                DefaultTopOverlayId = 1,
                DefaultBottomOverlayId = 1,
                CoverId = 3,
                Aoi = true,
                Comments = "Wysoka częstotliwość - ENIG required"
            }
        };
        context.Pcbs.AddRange(pcbs);
    }

    private static void SeedEmployees(MicroErpDbContext context)
    {
        var employees = new[]
        {
            new Employee { Id = 1, FirstName = "Michał", LastName = "Nowak", Login = "mnowak", IsActive = true },
            new Employee { Id = 2, FirstName = "Katarzyna", LastName = "Kowalczyk", Login = "kkowalczyk", IsActive = true },
            new Employee { Id = 3, FirstName = "Tomasz", LastName = "Wiśniewski", Login = "twisniewski", IsActive = true }
        };
        context.Employees.AddRange(employees);
    }

    private static void SeedOrders(MicroErpDbContext context)
    {
        var orders = new[]
        {
            new Order
            {
                Id = 1,
                OrdererName = "Jan Kowalski",
                PcbId = 1,
                OrderDate = DateTime.Today.AddDays(-14),
                OrderExpirationDate = DateTime.Today.AddDays(7),
                OrderedQty = 50,
                CustomerName = "TechnoElektro Sp. z o.o.",
                HolePlatting = true,
                Smt = true,
                SolderLayerTopId = 1,
                SolderLayerBottomId = 1,
                OverlayLayerTopId = 1,
                CoverTypeId = 2,
                OrderTypeId = 2,
                EmployeeId = 1,
                Express = false,
                Comments = "Standardowa seria produkcyjna"
            },
            new Order
            {
                Id = 2,
                OrdererName = "Anna Nowak",
                PcbId = 2,
                OrderDate = DateTime.Today.AddDays(-5),
                OrderExpirationDate = DateTime.Today.AddDays(21),
                OrderedQty = 10,
                CustomerName = "InnovaTech Kraków",
                SolderLayerTopId = 2,
                OverlayLayerTopId = 2,
                OrderTypeId = 1,
                EmployeeId = 2,
                Express = true,
                Comments = "Prototyp do testów - pilne"
            },
            new Order
            {
                Id = 3,
                OrdererName = "Piotr Wiśniewski",
                PcbId = 3,
                OrderDate = DateTime.Today.AddDays(-2),
                OrderExpirationDate = DateTime.Today.AddDays(14),
                OrderedQty = 100,
                CustomerName = "ElectroSoft",
                HolePlatting = true,
                Smt = true,
                Tht = true,
                SolderLayerTopId = 1,
                SolderLayerBottomId = 1,
                OverlayLayerTopId = 1,
                OverlayLayerBottomId = 1,
                CoverTypeId = 3,
                OrderTypeId = 2,
                EmployeeId = 3,
                Express = false,
                OrderedQtyToMount = 80,
                OrderedQtyToSell = 20,
                Comments = "Mieszana technologia montażu"
            }
        };
        context.Orders.AddRange(orders);
    }

    private static void SeedGuidebooks(MicroErpDbContext context)
    {
        var guidebooks = new[]
        {
            new Guidebook
            {
                Id = 1,
                OrderId = 1,
                FolderNumber = 2025001,
                Qty = 50,
                Riston = true,
                MosaicPrinting = true,
                Etching = true,
                Cutting = true,
                Control = true,
                Packing = false,
                Closed = false
            },
            new Guidebook
            {
                Id = 2,
                OrderId = 2,
                FolderNumber = 2025002,
                Qty = 10,
                Riston = false,
                MosaicPrinting = false,
                Etching = true,
                Cutting = true,
                Control = true,
                Packing = true,
                Closed = true
            }
        };
        context.Guidebooks.AddRange(guidebooks);
    }
}