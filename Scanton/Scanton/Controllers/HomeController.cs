using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scanton.Models;
using System.Diagnostics;
using static Scanton.Models.IndexViewModel;

namespace Scanton.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //http://techqunba-001-site5.atempurl.com/
        public async Task<IActionResult> Index()
        {
            // Get all products and banners from the database
            //var products = await _context.Products.ToListAsync();

            

            var banners = await _context.Banners.ToListAsync();
            var flash = await _context.FlashMessages.ToListAsync();
            var footersIcon = await _context.SocialMedia.ToListAsync();
            var footersDescription = await _context.FooterDescription.ToListAsync();
            var footerPages = await _context.Page.ToListAsync();
            var softbanners = await _context.SoftBanner.ToListAsync();
            var shortbanners = await _context.ShortBanner.ToListAsync();
            var parantCategories = await _context.ParentCategories.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products.ToListAsync();
            var recentProducts = await _context.Products.OrderByDescending(rp => rp.Created_Date).FirstOrDefaultAsync();
            var blogs = await _context.Blogs.ToListAsync();
            var rating=await _context.UserRating.ToListAsync();
            var video = await _context.VideoGallery.ToListAsync();
            var recentBlog = await _context.Blogs.OrderByDescending(rb=>rb.Id).FirstOrDefaultAsync();
            
            var bannerViewModels = banners.Select(b => new IndexViewModel.Banner
            {
                ImagePath = b.ImagePath, 
                Title = b.Title, 
                TittleParagraph1=b.TittleParagraph1,
                TittleParagraph2=b.TittleParagraph2
                
               
            }).ToList();


            var flashMessageViewModel = flash.Select(b => new IndexViewModel.FlashMessage
            {
                ImagePath = b.ImagePath,
                IsActive=b.IsActive

            }).ToList();

            var footerIconViewModels = footersIcon.Select(s => new IndexViewModel.SocialMedia
            {
                AccountIcon=s.AccountIcon,
                AccountName= s.AccountURLLink,
                AccountURLLink=s.AccountURLLink,
                Is_Active=s.Is_Active
                

            }).ToList();

            var footerDescriptionViewModels = footersDescription.Select(f => new IndexViewModel.FooterDescription
            {
                Title=f.Title,
                Description=f.Description,
                Is_Active = f.Is_Active


            }).ToList();

            var footerPagesViewModels = footerPages.Select(p => new IndexViewModel.Page
            {
                CustomePageData=p.CustomePageData,
                CustomePageTittle=p.CustomePageTittle,
                PageCategory=p.PageCategory,
                PageUrl=p.PageUrl,
                Is_Active=p.Is_Active


            }).ToList();

            var categoriess = _context.Categories
           .Include(c => c.ParentCategory) // Eagerly load the ParentCategory
           .ToList();

            var categoryViewModels = categories.Select(c => new IndexViewModel.Category
            {
                Id=c.Id,
                ImagePath = c.ImagePath,
                Name=c.Name,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : "No Parent" // Handle null case
            }).ToList();

            var ParentCategoryViewModels = parantCategories?.Select(pc => new IndexViewModel.ParentCategory
            {
                Id = pc.Id,
                Name = pc.Name,
                Description = pc.Description,
                ImagePath=pc.ImagePath,
                Categories = pc.Categories?.Select(c => new IndexViewModel.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImagePath = c.ImagePath,
                    ParentCategoryId = c.ParentCategoryId,
                    ParentCategoryName = pc.Name
                }).ToList() ?? new List<IndexViewModel.Category>()
            }).ToList() ?? new List<IndexViewModel.ParentCategory>();



            var productViewModels = products.Select(p => new IndexViewModel.Product
            {
                Id = p.Id,
                Name = p.Name,
                ImagePath = p.ImagePath,
                TittleBadge = p.TittleBadge,
                PriceWithDiscount = p.PriceWithDiscount,
                Price = p.Price,
                Flavor = p.Flavor,
                BadgeIsActive = p.BadgeIsActive,
                FeaturedProducts = p.FeaturedProducts,
                SubImage_1 = p.SubImage_1
            }).ToList();

            var userRatingViewModels = rating.Select(p => new IndexViewModel.UserRating
            {
                Id= p.Id,
               UserName = p.UserName,
               Rating = p.Rating,
               Comments = p.Comments,
                Is_Active = p.Is_Active
            }).ToList();

            var RecentBlogs = new IndexViewModel.Blog
            {
                ImagePath = recentBlog.ImagePath,
                Author = recentBlog.ImagePath,
                Content = recentBlog.Content,
                Title = recentBlog.Title,
                Blog_Category = recentBlog.Blog_Category
            };


            var RecentProductViewModels = new IndexViewModel.Product
            {
                Id = recentProducts.Id,
                Name = recentProducts.Name,
                ImagePath = recentProducts.ImagePath,
                TittleBadge = recentProducts.TittleBadge,
                PriceWithDiscount = recentProducts.PriceWithDiscount,
                Price = recentProducts.Price,
                Flavor = recentProducts.Flavor,
                SubImage_1 = recentProducts.SubImage_1,
                SubImage_2 = recentProducts.SubImage_2,
                SubImage_3 = recentProducts.SubImage_3,
                SubImage_4 = recentProducts.SubImage_4,
                BadgeIsActive = recentProducts.BadgeIsActive,
                Weight = recentProducts.Weight,
                PricePerPcs = recentProducts.PricePerPcs,
                FeaturedProducts = recentProducts.FeaturedProducts,
                CategoryId = recentProducts.CategoryId,
                Description = recentProducts.Description,
                ShortDescription = recentProducts.ShortDescription,
                // Assuming categories is already fetched, and you want the category name
                CategoryName = categories.FirstOrDefault(c => c.Id == recentProducts.CategoryId)?.Name ?? "Unknown"
            };



            var softBannerViewModel = softbanners.Select(sb => new IndexViewModel.SoftBanner
            {
                Banner_Image_1=sb.Banner_Image_1,
                Banner_Image_2=sb.Banner_Image_2,
                Banner_Image_3=sb.Banner_Image_3,
                Is_Active=sb.Is_Active
            }).ToList();

            var shortBannerViewModel = shortbanners.Select(shb=> new IndexViewModel.ShortBanner
            {
                Heading_Tittle=shb.Heading_Tittle,
                Sub_Tittle_Heading=shb.Sub_Tittle_Heading,
                Short_Banner=shb.Short_Banner,
                Is_Active=shb.Is_Active
            }).ToList();


            var blogsViewModel = blogs.Select(b => new IndexViewModel.Blog
            {
                ImagePath= b.ImagePath,
                Author=b.ImagePath,
                Content=b.Content,
                Title=b.Title,
                Blog_Category=b.Blog_Category

            }).ToList();

            var videoViewModel = video.Select(b => new IndexViewModel.VideoGallery
            {
                 Title=b.Title,
                 SubTitle=b.SubTitle,
                 VideoPath=b.VideoPath,
                 Is_Active=b.Is_Active
                 

            }).ToList();
            // Create a view model to pass to the view
            var viewModel = new IndexViewModel
            {
                Banners = bannerViewModels,
                Categories = categoryViewModels,
                Products = productViewModels,
                SoftBanners = softBannerViewModel,
                ShortBanners=shortBannerViewModel,
                Blogs=blogsViewModel,
                ParentCategories= ParentCategoryViewModels,
               UserRatings=userRatingViewModels,
               VideoGalleries=videoViewModel
            };
            ViewBag.RecentProduct = recentProducts;
            ViewBag.RecentBlogs = RecentBlogs;
            ViewBag.RecentProductId = recentProducts.Id;
            ViewBag.FlashImages = flashMessageViewModel;
            ViewBag.Banners = bannerViewModels;
            ViewBag.FooterIcon = footerIconViewModels;
            ViewBag.FooterDescription = footerDescriptionViewModels;
            ViewBag.FooterPage = footerPagesViewModels;
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> ContactUs()
        {

            var banners = await _context.Banners.ToListAsync();
            var flash = await _context.FlashMessages.ToListAsync();
            var footersIcon = await _context.SocialMedia.ToListAsync();
            var footersDescription = await _context.FooterDescription.ToListAsync();
            var footerPages = await _context.Page.ToListAsync();
            var softbanners = await _context.SoftBanner.ToListAsync();
            var shortbanners = await _context.ShortBanner.ToListAsync();
            var parantCategories = await _context.ParentCategories.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products.ToListAsync();
            var recentProducts = await _context.Products.OrderByDescending(rp => rp.Created_Date).FirstOrDefaultAsync();
            var blogs = await _context.Blogs.ToListAsync();
            var rating = await _context.UserRating.ToListAsync();
            var video = await _context.VideoGallery.ToListAsync();
            var recentBlog = await _context.Blogs.OrderByDescending(rb => rb.Id).FirstOrDefaultAsync();

            var ParentCategoryViewModels = parantCategories?.Select(pc => new IndexViewModel.ParentCategory
            {
                Id = pc.Id,
                Name = pc.Name,
                Description = pc.Description,
                ImagePath = pc.ImagePath,
                Categories = pc.Categories?.Select(c => new IndexViewModel.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImagePath = c.ImagePath,
                    ParentCategoryId = c.ParentCategoryId,
                    ParentCategoryName = pc.Name
                }).ToList() ?? new List<IndexViewModel.Category>()
            }).ToList() ?? new List<IndexViewModel.ParentCategory>();

            var footerIconViewModels = footersIcon.Select(s => new IndexViewModel.SocialMedia
            {
                AccountIcon = s.AccountIcon,
                AccountName = s.AccountURLLink,
                AccountURLLink = s.AccountURLLink,
                Is_Active = s.Is_Active


            }).ToList();

            var footerDescriptionViewModels = footersDescription.Select(f => new IndexViewModel.FooterDescription
            {
                Title = f.Title,
                Description = f.Description,
                Is_Active = f.Is_Active


            }).ToList();

            var footerPagesViewModels = footerPages.Select(p => new IndexViewModel.Page
            {
                CustomePageData = p.CustomePageData,
                CustomePageTittle = p.CustomePageTittle,
                PageCategory = p.PageCategory,
                PageUrl = p.PageUrl,
                Is_Active = p.Is_Active


            }).ToList();
            var viewModel = new IndexViewModel
            {

                ParentCategories = ParentCategoryViewModels,

            };

            ViewBag.FooterIcon = footerIconViewModels;
            ViewBag.FooterDescription = footerDescriptionViewModels;
            ViewBag.FooterPage = footerPagesViewModels;


            return View(viewModel);
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }


        


    }
}
