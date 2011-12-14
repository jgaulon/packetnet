﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpPcap.LibPcap;
using PacketDotNet;
using PacketDotNet.Utils;
using PacketDotNet.Ieee80211;

namespace Test.PacketType
{
    namespace Ieee80211
    {
        [TestFixture]
        public class BlockAcknowledgmentRequestFrameTest
        {
            /// <summary>
            /// Test that parsing a block acknowledgment request frame yields the proper field values
            /// </summary>
            [Test]
            public void Test_Constructor()
            {
                var dev = new CaptureFileReaderDevice("../../CaptureFiles/80211_block_acknowledgment_request_frame.pcap");
                dev.Open();
                var rawCapture = dev.GetNextPacket();
                dev.Close();

                Packet p = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                BlockAcknowledgmentRequestFrame frame = (BlockAcknowledgmentRequestFrame)p.PayloadPacket;

                Assert.AreEqual(0, frame.FrameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ControlBlockAcknowledgmentRequest, frame.FrameControl.Type);
                Assert.IsFalse(frame.FrameControl.ToDS);
                Assert.IsFalse(frame.FrameControl.FromDS);
                Assert.IsFalse(frame.FrameControl.MoreFragments);
                Assert.IsFalse(frame.FrameControl.Retry);
                Assert.IsFalse(frame.FrameControl.PowerManagement);
                Assert.IsFalse(frame.FrameControl.MoreData);
                Assert.IsFalse(frame.FrameControl.Wep);
                Assert.IsFalse(frame.FrameControl.Order);
                Assert.AreEqual(314, frame.Duration.Field); //this need expanding on in the future
                Assert.AreEqual("7CC5376D16E7", frame.ReceiverAddress.ToString().ToUpper());
                Assert.AreEqual("0024B2F8D706", frame.TransmitterAddress.ToString().ToUpper());

                Assert.AreEqual(BlockAcknowledgmentControlField.AcknowledgementPolicy.Delayed, frame.BlockAcknowledgmentControl.Policy);
                Assert.IsFalse(frame.BlockAcknowledgmentControl.MultiTid);
                Assert.IsTrue(frame.BlockAcknowledgmentControl.CompressedBitmap);
                Assert.AreEqual(0, frame.BlockAcknowledgmentControl.Tid);
                Assert.AreEqual(0x0000, frame.BlockAckStartingSequenceControl);

                Assert.AreEqual(0x471D197A, frame.FrameCheckSequence);
                Assert.AreEqual(20, frame.FrameSize);
            }
        } 
    }
}
