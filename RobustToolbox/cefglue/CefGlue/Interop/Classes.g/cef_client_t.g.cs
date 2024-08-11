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
    internal unsafe struct cef_client_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_client_t*, cef_audio_handler_t*> _get_audio_handler;
        internal delegate* unmanaged<cef_client_t*, cef_command_handler_t*> _get_command_handler;
        internal delegate* unmanaged<cef_client_t*, cef_context_menu_handler_t*> _get_context_menu_handler;
        internal delegate* unmanaged<cef_client_t*, cef_dialog_handler_t*> _get_dialog_handler;
        internal delegate* unmanaged<cef_client_t*, cef_display_handler_t*> _get_display_handler;
        internal delegate* unmanaged<cef_client_t*, cef_download_handler_t*> _get_download_handler;
        internal delegate* unmanaged<cef_client_t*, cef_drag_handler_t*> _get_drag_handler;
        internal delegate* unmanaged<cef_client_t*, cef_find_handler_t*> _get_find_handler;
        internal delegate* unmanaged<cef_client_t*, cef_focus_handler_t*> _get_focus_handler;
        internal delegate* unmanaged<cef_client_t*, cef_frame_handler_t*> _get_frame_handler;
        internal delegate* unmanaged<cef_client_t*, cef_permission_handler_t*> _get_permission_handler;
        internal delegate* unmanaged<cef_client_t*, cef_jsdialog_handler_t*> _get_jsdialog_handler;
        internal delegate* unmanaged<cef_client_t*, cef_keyboard_handler_t*> _get_keyboard_handler;
        internal delegate* unmanaged<cef_client_t*, cef_life_span_handler_t*> _get_life_span_handler;
        internal delegate* unmanaged<cef_client_t*, cef_load_handler_t*> _get_load_handler;
        internal delegate* unmanaged<cef_client_t*, cef_print_handler_t*> _get_print_handler;
        internal delegate* unmanaged<cef_client_t*, cef_render_handler_t*> _get_render_handler;
        internal delegate* unmanaged<cef_client_t*, cef_request_handler_t*> _get_request_handler;
        internal delegate* unmanaged<cef_client_t*, cef_browser_t*, cef_frame_t*, CefProcessId, cef_process_message_t*, int> _on_process_message_received;
        
        internal GCHandle _obj;
        
        [UnmanagedCallersOnly]
        public static void add_ref(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            obj.add_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int release(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.release(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_one_ref(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.has_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_at_least_one_ref(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.has_at_least_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_audio_handler_t* get_audio_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_audio_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_command_handler_t* get_command_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_command_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_context_menu_handler_t* get_context_menu_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_context_menu_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_dialog_handler_t* get_dialog_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_dialog_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_display_handler_t* get_display_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_display_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_download_handler_t* get_download_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_download_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_drag_handler_t* get_drag_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_drag_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_find_handler_t* get_find_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_find_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_focus_handler_t* get_focus_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_focus_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_frame_handler_t* get_frame_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_frame_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_permission_handler_t* get_permission_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_permission_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_jsdialog_handler_t* get_jsdialog_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_jsdialog_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_keyboard_handler_t* get_keyboard_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_keyboard_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_life_span_handler_t* get_life_span_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_life_span_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_load_handler_t* get_load_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_load_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_print_handler_t* get_print_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_print_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_render_handler_t* get_render_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_render_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static cef_request_handler_t* get_request_handler(cef_client_t* self)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.get_request_handler(self);
        }
        
        [UnmanagedCallersOnly]
        public static int on_process_message_received(cef_client_t* self, cef_browser_t* browser, cef_frame_t* frame, CefProcessId source_process, cef_process_message_t* message)
        {
            var obj = (CefClient)self->_obj.Target;
            return obj.on_process_message_received(self, browser, frame, source_process, message);
        }
        
        internal static cef_client_t* Alloc(CefClient obj)
        {
            var ptr = (cef_client_t*)NativeMemory.Alloc((UIntPtr)sizeof(cef_client_t));
            *ptr = default(cef_client_t);
            ptr->_base._size = (UIntPtr)sizeof(cef_client_t);
            ptr->_obj = GCHandle.Alloc(obj);
            ptr->_base._add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)(delegate* unmanaged<cef_client_t*, void>)&add_ref;
            ptr->_base._release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_client_t*, int>)&release;
            ptr->_base._has_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_client_t*, int>)&has_one_ref;
            ptr->_base._has_at_least_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_client_t*, int>)&has_at_least_one_ref;
            ptr->_get_audio_handler = &get_audio_handler;
            ptr->_get_command_handler = &get_command_handler;
            ptr->_get_context_menu_handler = &get_context_menu_handler;
            ptr->_get_dialog_handler = &get_dialog_handler;
            ptr->_get_display_handler = &get_display_handler;
            ptr->_get_download_handler = &get_download_handler;
            ptr->_get_drag_handler = &get_drag_handler;
            ptr->_get_find_handler = &get_find_handler;
            ptr->_get_focus_handler = &get_focus_handler;
            ptr->_get_frame_handler = &get_frame_handler;
            ptr->_get_permission_handler = &get_permission_handler;
            ptr->_get_jsdialog_handler = &get_jsdialog_handler;
            ptr->_get_keyboard_handler = &get_keyboard_handler;
            ptr->_get_life_span_handler = &get_life_span_handler;
            ptr->_get_load_handler = &get_load_handler;
            ptr->_get_print_handler = &get_print_handler;
            ptr->_get_render_handler = &get_render_handler;
            ptr->_get_request_handler = &get_request_handler;
            ptr->_on_process_message_received = &on_process_message_received;
            return ptr;
        }
        
        internal static void Free(cef_client_t* ptr)
        {
            ptr->_obj.Free();
            NativeMemory.Free((void*)ptr);
        }
        
    }
}
