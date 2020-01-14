﻿using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.EntityManagement.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Tests
{
    [TestClass]
    public class SfQueryServiceTests
    {
        [TestMethod]
        public void GetStringifiedQuery_WhenQueryRequestHasOnlyParent_ReturnsStringQueryWithOnlyParentWithoutSearchAndSort()
        {
            var request = new ExtractMap
            {
                Name = "Test Name",
                Query = new ReadableMapExtract
                {
                    Parent = new ReadableObject
                    {
                        Fields = new List<SfField>
                        {
                            new SfField{
                                ApiName="ApiName1",
                                Name="LabelOne"
                            },
                             new SfField{
                                ApiName="ApiName2",
                                Name="LabelTwo"
                            },
                              new SfField{
                                ApiName="ApiName3",
                                Name="LabelThree"
                            }
                        },
                        Label = "ParentOne"
                    },
                }
            };

            var service = new SfQueryService();

            var response = service.GetStringifiedQuery(request);

            Assert.AreEqual("SELECT LabelOne,LabelTwo,LabelThree FROM ParentOne", response);
        }

        [TestMethod]
        public void GetStringifiedQuery_WhenQueryRequestHasOnlyParent_ReturnsStringQueryWithOnlyParent()
        {
            var request = new ExtractMap
            {
                Name = "Test Name",
                Query = new ReadableMapExtract
                {
                    Parent = new ReadableObject
                    {
                        Fields = new List<SfField>
                        {
                            new SfField{
                                ApiName="ApiName1",
                                Name="LabelOne"
                            },
                             new SfField{
                                ApiName="ApiName2",
                                Name="LabelTwo"
                            },
                              new SfField{
                                ApiName="ApiName3",
                                Name="LabelThree"
                            },
                        },
                        SearchFilter = "ApiName2='1234'",
                        SortFilter = "ApiName DESC",
                        Label = "ParentOne"
                    },
                }
            };

            var service = new SfQueryService();

            var response = service.GetStringifiedQuery(request);

            Assert.AreEqual("SELECT LabelOne,LabelTwo,LabelThree FROM ParentOne WHERE ApiName2='1234' ORDER BY ApiName DESC", response);
        }

        [TestMethod]
        public void GetStringifiedQuery_WhenQueryRequestHasChildren_ReturnsStringQuery()
        {
            var request = new ExtractMap
            {
                Name = "Test Name",
                Query = new ReadableMapExtract
                {
                    Parent = new ReadableObject
                    {
                        Fields = new List<SfField>
                        {
                            new SfField{
                                ApiName="ApiName1",
                                Name="LabelOne"
                            },
                             new SfField{
                                ApiName="ApiName2",
                                Name="LabelTwo"
                            },
                              new SfField{
                                ApiName="ApiName3",
                                Name="LabelThree"
                            },
                        },
                        SearchFilter = "ApiName2='1234'",
                        SortFilter = "ApiName DESC",
                        Label = "ParentOne"
                    },
                    Children = new List<ReadableObject>
                    {
                        new ReadableObject
                    {
                        Fields = new List<SfField>
                        {
                            new SfField{
                                ApiName="ApiName1",
                                Name="LabelOne"
                            },
                             new SfField{
                                ApiName="ApiName2",
                                Name="LabelTwo"
                            }
                        },
                        SearchFilter = "ApiName2='1234'",
                        SortFilter = "ApiName DESC",
                        Label = "ChildOne"
                    },
                        new ReadableObject
                    {
                        Fields = new List<SfField>
                        {
                            new SfField{
                                ApiName="ApiName1",
                                Name="LabelOne"
                            }
                        },
                        SearchFilter = "ApiName1='1234'",
                        SortFilter = "ApiName DESC",
                        Label = "ChildTwo"
                    }
                    }
                }
            };

            var service = new SfQueryService();

            var response = service.GetStringifiedQuery(request);

            Assert.AreEqual("SELECT LabelOne,LabelTwo,LabelThree,(SELECT LabelOne,LabelTwo FROM ChildOne WHERE ApiName2='1234' ORDER BY ApiName DESC),(SELECT LabelOne FROM ChildTwo WHERE ApiName1='1234' ORDER BY ApiName DESC) FROM ParentOne WHERE ApiName2='1234' ORDER BY ApiName DESC", response);
        }
    }
}