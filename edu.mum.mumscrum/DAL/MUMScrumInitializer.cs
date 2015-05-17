using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.DAL
{
    public class MUMScrumInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MUMScrumContext>
    {
        protected override void Seed(MUMScrumContext context)
        {
            //var productBacklogs = new List<ProductBacklog>
            //{
            //    new ProductBacklog{ Name = "Test Product Backlog 1", Description = "This is test product backlog 1", CreatedBy = 1, CreatedDate = DateTime.Parse("2015-13-05")},
            //    new ProductBacklog{ Name = "Test Product Backlog 2", Description = "This is test product backlog 2", CreatedBy = 1, CreatedDate = DateTime.Parse("2015-13-05")}
            //};

            //productBacklogs.ForEach(pb => context.ProductBacklogs.Add(pb));
            //context.SaveChanges();

            #region
            //11	Product Backlog 1	This is product back log 1	1	05/14/2015 2:14:21 PM	NULL	NULL	NULL

                //25	User Story 1	This is user story 1	1	05/14/2015 2:14:56 PM	NULL	NULL	NULL	0	0	5	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //26	User Story 2	This is user story 2	1	05/14/2015 2:15:19 PM	NULL	NULL	NULL	0	0	5	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //27	User Story 3	This is user story 3	1	05/14/2015 2:15:32 PM	NULL	NULL	NULL	0	0	5	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //28	User Story 4	This is user story 4	1	05/14/2015 2:15:50 PM	NULL	NULL	NULL	0	0	6	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //29	User Story 5	This is user story 5	1	05/14/2015 2:16:07 PM	NULL	NULL	NULL	0	0	6	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //30	User Story 6	This is user story 6	1	05/14/2015 2:16:23 PM	NULL	NULL	NULL	0	0	6	10	11	NULL	NULL	NULL	NULL	NULL	NULL
                //31	User Story 7	This is user story 7	1	05/14/2015 2:16:36 PM	NULL	NULL	NULL	0	0	NULL	11	11	NULL	NULL	NULL	NULL	NULL	NULL
                //32	User Story 8	This is user story 8	1	05/14/2015 2:16:48 PM	NULL	NULL	NULL	0	0	NULL	11	11	NULL	NULL	NULL	NULL	NULL	NULL

                //10	Release Backlog 1	This is Release Backlog 1	1	05/14/2015 2:17:25 PM	NULL	NULL	NULL	11	NULL
                //11	Release Backlog 2	This is Release Backlog 2	1	05/14/2015 2:17:47 PM	NULL	NULL	NULL	11	NULL

                //5	Sprint 1	This is sprint 1 for release backlog 1	1	05/14/2015 6:19:34 PM	NULL	NULL	NULL	10
            //6	Sprint 2	This is sprint 2 for release backlog 1	1	05/14/2015 6:24:03 PM	NULL	NULL	NULL	10

            //1	Vice President Marketing
            //2	Marketing Manager
            //3	Senior Software Engineer
            //4	Software Engineer
            //5	Software Test Engineer
            //6	HR Administrator

            //1	Kashif	Shabih	0	1	05/15/2015 7:53:35 PM	05/15/2015 12:00:00 AM	1	0	kashif	shabih	1
            //2	Sherif	Ahmed	0	1	05/15/2015 7:54:44 PM	05/15/2015 12:00:00 AM	3	0	sherif	ahmed	NULL
            //3	HR	administrator	0	1	05/15/2015 7:55:40 PM	05/15/2015 12:00:00 AM	6	0	admin	admin	0

            #endregion
        }
    }
}