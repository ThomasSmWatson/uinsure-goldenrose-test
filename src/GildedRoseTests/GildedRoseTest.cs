﻿using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {

        // Standard test against core business logic
        [Fact]
        public void GivenItem_WhenQualityUpdated_QualityDegrades()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "TestItem",
                    Quality = 10,
                    SellIn = 1
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(9, items[0].Quality);
        }

        //- The `Quality` of an item is never negative
        [Fact]
        public void GivenItemWithQualityZero_WhenQualityUpdated_QualityStaysZero()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "TestItem",
                    Quality = 0,
                    SellIn = 1
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }


        //    - Once the sell by date has passed, `Quality` degrades twice as fast
        [Fact]
        public void GivenItem_WithSellByDatePassed_QualityDegradesTwiceASFast()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "TestItem",
                    Quality = 10,
                    SellIn = 0
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(8, items[0].Quality);
        }

        //- __"Aged Brie"__ actually increases in `Quality` the older it gets
        [Fact]
        public void GivenAgedBrieItem_WhenQualityUpdated_QualityImproves()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Aged Brie",
                    Quality = 10,
                    SellIn = 1
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(11, items[0].Quality);
        }

        //- The `Quality` of an item is never more than `50`
        [Fact]
        public void GivenAgedBrieItemWithQualityFifty_WhenQualityUpdated_QualityStaysAtFifty()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Aged Brie",
                    Quality = 50,
                    SellIn = 1
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(50, items[0].Quality);
        }

        //- __"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
        //"Sulfuras"__ is a legendary item and as such its `Quality` is `80` and it never alters.
        [Fact]
        public void GivenSulfurasItem_WhenQualityUpdated_QualityOrSellInStaysTheSame()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Sulfuras, Hand of Ragnaros",
                    Quality = 80,
                    SellIn = 3
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(80, items[0].Quality);
            Assert.Equal(3, items[0].SellIn);
        }

        //- __"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
        //	- `Quality` increases by `2` when there are `10` days or less
        [Fact]
        public void GivenBackstagePassItemWithTenOrLessDaysLeft_WhenQualityUpdated_QualityIncreasesByTwo()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    Quality = 10,
                    SellIn = 7
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(12, items[0].Quality);
        }

        //- __"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
        //	- `Quality` increases by `3` when there are `5` days or less 
        [Fact]
        public void GivenBackstagePassItemWithFiveOrLessDaysLeft_WhenQualityUpdated_QualityIncreasesByThree()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    Quality = 10,
                    SellIn = 5
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(13, items[0].Quality);
        }


        //- __"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
        //	- `Quality` drops to `0` after the concert 
        [Fact]
        public void GivenBackstagePassItemWithZeroSellIn_WhenQualityUpdated_QualityDropsToZero()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    Quality = 10,
                    SellIn = 0
                }
            };
            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        //- __"Conjured"__ items degrade in `Quality` twice as fast as normal items
        //[Fact]
        public void GivenConjuredItem_WhenQualityUpdated_QualityDropsTwiceAsFastAsNormal()
        {
            var items = new List<Item>() {
                new Item()
                {
                    Name = "Conjured item 123",
                    Quality = 10,
                    SellIn = 4
                },
                    new Item()
                {
                    Name = "NormalItem123",
                    Quality = 10,
                    SellIn = 4
                }
            };

            var sut = new GildedRoseKata.GildedRose(items);

            sut.UpdateQuality();
            Assert.Equal(8, items[0].Quality);
            Assert.Equal(9, items[1].Quality);
            sut.UpdateQuality();
            Assert.Equal(6, items[0].Quality);
            Assert.Equal(8, items[1].Quality);
            sut.UpdateQuality();
            Assert.Equal(4, items[0].Quality);
            Assert.Equal(7, items[1].Quality);
        }
    }
}
