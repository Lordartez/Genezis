﻿//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Xilium.CefGlue.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_media_route_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_media_route_t*, cef_string_userfree*> _get_id;
        internal delegate* unmanaged<cef_media_route_t*, cef_media_source_t*> _get_source;
        internal delegate* unmanaged<cef_media_route_t*, cef_media_sink_t*> _get_sink;
        internal delegate* unmanaged<cef_media_route_t*, void*, UIntPtr, void> _send_route_message;
        internal delegate* unmanaged<cef_media_route_t*, void> _terminate;
        
        // AddRef
        
        public static void add_ref(cef_media_route_t* self)
        {
            self->_base._add_ref((cef_base_ref_counted_t*)self);
        }
        
        // Release
        
        public static int release(cef_media_route_t* self)
        {
            return self->_base._release((cef_base_ref_counted_t*)self);
        }
        
        // HasOneRef
        
        public static int has_one_ref(cef_media_route_t* self)
        {
            return self->_base._has_one_ref((cef_base_ref_counted_t*)self);
        }
        
        // HasAtLeastOneRef
        
        public static int has_at_least_one_ref(cef_media_route_t* self)
        {
            return self->_base._has_at_least_one_ref((cef_base_ref_counted_t*)self);
        }
        
        // GetId
        
        public static cef_string_userfree* get_id(cef_media_route_t* self)
        {
            return self->_get_id(self);
        }
        
        // GetSource
        
        public static cef_media_source_t* get_source(cef_media_route_t* self)
        {
            return self->_get_source(self);
        }
        
        // GetSink
        
        public static cef_media_sink_t* get_sink(cef_media_route_t* self)
        {
            return self->_get_sink(self);
        }
        
        // SendRouteMessage
        
        public static void send_route_message(cef_media_route_t* self, void* message, UIntPtr message_size)
        {
            self->_send_route_message(self, message, message_size);
        }
        
        // Terminate
        
        public static void terminate(cef_media_route_t* self)
        {
            self->_terminate(self);
        }
        
    }
}
