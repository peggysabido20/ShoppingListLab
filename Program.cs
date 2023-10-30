namespace ShoppingListLab
{
    internal class Program
    {
        class MenuDetail
        {
            public string ItemName { get; set; }
            public double ItemPrice { get; set; }
        }
        static void Main(string[] args)
        {
            var menuList = new Dictionary<string, MenuDetail>()
            {
                { "a", new MenuDetail { ItemName = "apple", ItemPrice = .99 } },
                { "b", new MenuDetail { ItemName = "banana", ItemPrice = .59}},
                { "c", new MenuDetail { ItemName = "cauliflower", ItemPrice = 1.59}},
                { "d", new MenuDetail { ItemName = "dragonfruit", ItemPrice = 2.19}},
                { "e", new MenuDetail { ItemName = "elderberry", ItemPrice = 1.79}},
                { "f", new MenuDetail { ItemName = "figs", ItemPrice = 2.09}},
                { "g", new MenuDetail { ItemName = "grapefruit", ItemPrice = 1.99}},
                { "h", new MenuDetail { ItemName = "honeydew", ItemPrice = 3.49}}
            };

            Dictionary<string, double> openWith = new Dictionary<string, double>();
            openWith.Add("apple", .99);
            openWith.Add("banana", .59);
            openWith.Add("cauliflower", 1.59);
            openWith.Add("dragonfruit", 2.19);
            openWith.Add("elderberry", 1.79);
            openWith.Add("figs", 2.09);
            openWith.Add("grapefruit", 1.99);
            openWith.Add("honeydew", 3.49);

            // Is there a way to access MenuDetail entries added in menuList witout the key on line 14? Instead of creating another dictonary with the same entries.
            // I'm sure there's a better way to do this, instead of repeating things

            SortedList<double, string> sortedPurchaseList = new SortedList<double, string>();
            List<string> menuItems = new List<string>();
            List<double> menuAmounts = new List<double>();
            string userItem = "";

            Console.WriteLine("Welcome to Chirpus Market!");
            while (true)
            {
                printMenuList(menuList);
                Console.WriteLine();
                Console.Write("What item would you like to order? Please enter the item code or the item name: ");
                userItem = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(userItem))
                {
                    if (userItem.Length == 1) // By code
                    {
                        if (menuList.ContainsKey(userItem.ToLower()))
                        {
                            createPurchaseList(menuItems, menuAmounts, menuList[userItem.ToLower()].ItemName, menuList[userItem.ToLower()].ItemPrice);

                        }
                        else
                        {
                            Console.WriteLine("Sorry, we don't have those. Please try again");
                        }
                    } // if (userItem.Length == 1)
                    else // By name
                    {
                        if (openWith.ContainsKey(userItem.ToLower()))
                        {
                            openWith.TryGetValue(userItem.ToLower(), out double itemValue);
                            createPurchaseList(menuItems, menuAmounts, userItem.ToLower(), itemValue);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, we don't have those. Please try again");
                        }
                    } // else if (userItem.Length == 1)
                } // if (!string.IsNullOrEmpty(userItem))

                if (!wantToContinue())
                {
                    break;
                }
            }
            Console.WriteLine();
            double sumItems = menuAmounts.Sum();
            int indexItem = 0;
            if (sumItems > 0)
            {
                Console.WriteLine("Thanks for your order!");
                Console.WriteLine("Here's what you got:");

                foreach (string item in menuItems)
                {
                    indexItem = menuItems.IndexOf(item);
                    sortedPurchaseList.Add(menuAmounts[indexItem], item);
                }

                foreach (KeyValuePair<double, string> kvp in sortedPurchaseList)
                {
                    Console.WriteLine($"{kvp.Value,-14}${kvp.Key:F2}");
                }

                Console.WriteLine($"The total of your order is : {sumItems:F2}");                
                indexItem = sortedPurchaseList.Count - 1;
                Console.WriteLine($"The more expensive item your ordered is : {sortedPurchaseList.Values[indexItem]} at ${sortedPurchaseList.Keys[indexItem]}");
                Console.WriteLine($"The more expensive item your ordered is : {sortedPurchaseList.Values[0]} at ${sortedPurchaseList.Keys[0]}");
                Console.WriteLine($"The total of your order is : ${menuAmounts.Sum():F2}");
                Console.WriteLine($"Average price per item in order was: ${menuAmounts.Average():F2}");
            } // if (sumItems > 0)
        } // static void Main(string[] args)

        static void printMenuList(Dictionary<string, MenuDetail> menuList)
        {
            Dictionary<string, MenuDetail>.KeyCollection keyColl = menuList.Keys;
            const int PADDING = -14;
            Console.WriteLine();
            Console.WriteLine($"{"ItemCode",PADDING}{"ItemName",PADDING}Price");
            Console.WriteLine($"=================================");
            foreach (string keyItem in keyColl)
            {
                Console.WriteLine($"{keyItem, PADDING}{menuList[keyItem].ItemName, PADDING}{menuList[keyItem].ItemPrice}");
                
            }
        }

        static bool wantToContinue()
        {
            while (true)
            {
                Console.Write("Would you like to order anything else (y/n)? ");
                string answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && (answer.Trim().ToLower() == "n" || answer.Trim().ToLower() == "y"))
                {
                    return (answer.Trim().ToLower() == "y");
                }
                Console.Write("Invalid entry, please enter y/n. Press any key .....");
                string AnyKey = Console.ReadLine();
            }
        }

        static void createPurchaseList(List<string> menuItems, List<double> menuAmounts, string itemName, double itemPrice)
        {
            int itemIndex = menuItems.IndexOf(itemName);
            if (itemIndex < 0)
            {
                menuItems.Add(itemName);
                menuAmounts.Add(itemPrice);
            }
            else
            {
                menuAmounts[itemIndex] += itemPrice;
            }
            Console.WriteLine($"Adding {itemName} to cart at ${itemPrice}");
        } // static void createPurchaseList
    } // internal class Program
} // namespace ShoppingListLab