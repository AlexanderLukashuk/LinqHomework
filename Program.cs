using System;
using HWLinq.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace HWLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appSettings.json")
               .Build();

            var connectionString = configuration.GetSection("connectionStrings")["deliveryofproducts"];
            int option = -1;
            string input;
            const int PRODUCT_PER_PAGE = 5;
            int currentPage = 0;
            int page;

            using (ApplicationContext context = new ApplicationContext(connectionString))
            {
                while (option != 4)
                {
                    Console.WriteLine("Выберите опцию:");
                    Console.WriteLine("1)Следующая страница");
                    Console.WriteLine("2)Предыдущая страница");
                    Console.WriteLine("3)Выбрать самому страницу");
                    Console.WriteLine("4)Выход");
                    input = Console.ReadLine();
                    option = int.Parse(input);

                    switch (option)
                    {
                        case 1:
                            {
                                ShowNextPage(context, PRODUCT_PER_PAGE, ref currentPage);
                                break;
                            }
                        case 2:
                            {
                                ShowPrevPage(context, PRODUCT_PER_PAGE, ref currentPage);
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Введите номер страницы: ");
                                input = Console.ReadLine();
                                page = int.Parse(input);
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Good bye");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Вы выбрали неправильную опцию");
                                Console.WriteLine("Попробуйте выбрать еще раз");
                                break;
                            }
                    }
                }
            }
        }

        public static void ShowNextPage(ApplicationContext context, int productsPerPage, ref int currentPage)
        {
            currentPage++;
            var page = context.Products.Skip(productsPerPage * (currentPage - 1)).Take(productsPerPage).ToList();
            Console.WriteLine(page.ToString());
        }

        public static void ShowPrevPage(ApplicationContext context, int productsPerPage, ref int currentPage)
        {
            currentPage--;
            var page = context.Products.Skip(productsPerPage * (currentPage - 1)).Take(productsPerPage).ToList();
            Console.WriteLine(page);
        }

        public static void ShowPage(ApplicationContext context, int productsPerPage, ref int currentPage, int userPage)
        {
            currentPage = userPage;
            var page = context.Products.Skip(productsPerPage * (currentPage - 1)).Take(productsPerPage).ToList();
            Console.WriteLine(page.ToString());
        }
    }
}
